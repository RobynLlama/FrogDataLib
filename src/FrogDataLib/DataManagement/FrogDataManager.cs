using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace FrogDataLib.DataManagement;

/// <summary>
/// The static container for all saving and loading events and methods
/// </summary>
public static class FrogDataManager
{
  internal static readonly DirectoryInfo PersistentPath = new(Path.Combine(Application.persistentDataPath, "FrogData"));
  internal static JsonSerializerSettings SerialSettings = new()
  {
    TypeNameHandling = TypeNameHandling.Auto,
    ObjectCreationHandling = ObjectCreationHandling.Replace
  };

  private static Dictionary<string, string> MasterData = [];

  /// <summary>
  /// Fired when the user requests a game save, consumers are expected
  /// to respond with their mod's save object to be stored in the
  /// FrogData save file
  /// </summary>
  public static event Action? OnBeginSaving;

  /// <summary>
  /// Fired when the user has finished loading a game and it is now safe
  /// to request your slot specific mod data object from FrogDataLib
  /// </summary>
  public static event Action? OnLoadCompleted;

  /// <summary>
  /// Fired when the user quits to the main menu and it is safe to destroy
  /// and reset your mod's state and wait
  /// </summary>
  public static event Action? OnSessionEnded;

  internal static void SaveToSlot(int slot)
  {
    FrogDataPlugin.Log.LogMessage($"Saving slot {slot}");

    //Inform consumers that we are now saving
    OnBeginSaving?.Invoke();

    FileInfo dataPath = new(Path.Combine(PersistentPath.FullName, $"slot_{slot}.dat"));

    try
    {
      var data = JsonConvert.SerializeObject(MasterData);
      using FileStream fs = new(dataPath.FullName, FileMode.Create, FileAccess.Write);
      using StreamWriter writer = new(fs, Encoding.UTF8);
      writer.Write(data);
      writer.Flush();
    }
    catch (Exception ex)
    {
      FrogDataPlugin.Log.LogError($"Unable to save data to slot {slot}\n\nFull Text: {ex.Message}");
      return;
    }
  }

  internal static void LoadFromSlot(int slot)
  {
    FrogDataPlugin.Log.LogMessage($"Loading slot {slot}");
    FileInfo dataPath = new(Path.Combine(PersistentPath.FullName, $"slot_{slot}.dat"));

    if (!dataPath.Exists)
    {
      FrogDataPlugin.Log.LogInfo($"Skipping slot {slot}, no data to load");
      return;
    }

    try
    {
      using StreamReader reader = new(dataPath.FullName);
      string data = reader.ReadToEnd();

      if (JsonConvert.DeserializeObject<Dictionary<string, string>>(data) is not Dictionary<string, string> value)
        MasterData = [];
      else
        MasterData = value;
    }
    catch (Exception ex)
    {
      FrogDataPlugin.Log.LogError($"Unable to load data from slot {slot}\n\nFull Text: {ex.Message}");
      return;
    }

    //Inform consumers it is now save to request save data
    OnLoadCompleted?.Invoke();
  }

  internal static void DeleteSlot(int slot)
  {
    FrogDataPlugin.Log.LogMessage($"Deleting slot {slot}");
    FileInfo dataPath = new(Path.Combine(PersistentPath.FullName, $"slot_{slot}.dat"));

    if (dataPath.Exists)
      dataPath.Delete();
  }

  internal static void SessionEnded()
  {
    FrogDataPlugin.Log.LogMessage("The session has ended");
    OnSessionEnded?.Invoke();
    MasterData.Clear();
  }


  internal static bool TryGetModData<TModel>(string guid, [NotNullWhen(true)] out TModel? value) where TModel : FrogDataModel, new()
  {
    value = null;

    if (!MasterData.TryGetValue(guid, out var data))
      return false;

    try
    {
      if (JsonConvert.DeserializeObject<TModel>(data) is not TModel item)
      {
        FrogDataPlugin.Log.LogError($"Unable to deserialize model for {guid} because the response from Newtonsoft.JSON was null. Data may be invalid or corrupt.");
        return false;
      }

      value = item;
      value.OnAfterSerialize();
    }
    catch (Exception ex)
    {
      FrogDataPlugin.Log.LogError($"Failed to deserialize data for {guid}.\n\nFull Text: {ex.Message}");
      return false;
    }

    if (value._frogSentinel == 0)
    {
      var dataType = typeof(TModel).Name;

      FrogDataPlugin.Log.LogError($"""
      Failed to deserialize data for {guid} due to a sentinel failure, likely causes:
        1. Class '{dataType}' may not be marked [Serializable]
        2. The save data for {guid} may have been corrupted, damaged or from a different/incompatible version
      """);

      value = null;
      return false;
    }

    return true;
  }

  internal static bool TrySaveModData<TModel>(string guid, TModel data) where TModel : FrogDataModel, new()
  {
    if (data._frogSentinel == 0)
    {
      FrogDataPlugin.Log.LogWarning($"Ignoring an invalid save model from {guid} due to a sentinel failure. This is likely a broken object, save data will not be updated!");
      return false;
    }

    if (!typeof(TModel).IsSerializable)
    {
      FrogDataPlugin.Log.LogWarning($"""
      Ignoring an invalid save model from {guid}!
        The class '{typeof(TModel).Name}' is not marked as serializable and will never save correctly!
        Please reference the FrogDataLib documentation @ https://github.com/RobynLlama/FrogDataLib for more information on structuring your save data
      """);
      return false;
    }

    //In case somebody messed with the sentinel value
    data._frogSentinel = 8675309;

    try
    {
      data.OnBeforeSerialize();
      var text = JsonConvert.SerializeObject(data);
      MasterData[guid] = text;
    }
    catch (Exception ex)
    {
      FrogDataPlugin.Log.LogError($"An error was encountered while trying to save a model from {guid}\n\nFull Text: {ex.Message}");
      return false;
    }

    return true;
  }
}
