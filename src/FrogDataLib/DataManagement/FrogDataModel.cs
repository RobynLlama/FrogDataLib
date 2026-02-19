#pragma warning disable CS0414

using System;
using UnityEngine;

namespace FrogDataLib.DataManagement;

/// <summary>
/// The base data model from which all save data must derive. This
/// is in order to have the sentinel value for integrity checking
/// </summary>
[Serializable]
public abstract class FrogDataModel
{
  [Obsolete("Legacy artifact of JsonUtility, not used for anything")]
  [JsonProperty("_frogSentinel")]
  private int FrogSentinel = 0;

  /// <summary>
  /// This is used to suppress the old _frogSentinel value from
  /// showing up in saved data anymore as UnitySerialization is
  /// no longer used
  /// </summary>
  /// <returns>FALSE</returns>
  public bool ShouldSerializeFrogSentinel()
  {
    FrogDataPlugin.Log.LogMessage("Should Serialize Callback for FrogSentinel");
    return false;
  }

  /// <summary>
  /// This method is called after your data is loaded
  /// </summary>
  public virtual void OnAfterSerialize() { }

  /// <summary>
  /// This method is called before passing your data model
  /// to Newtonsoft
  /// </summary>
  public virtual void OnBeforeSerialize() { }
}
