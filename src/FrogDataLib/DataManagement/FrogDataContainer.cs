namespace FrogDataLib.DataManagement;

/// <summary>
/// A Data container for easily saving and loading your mod data
/// in response to save or load events.
/// </summary>
/// <typeparam name="TModel">Your data model type</typeparam>
/// <param name="guid">A globally unique ID</param>
public class FrogDataContainer<TModel>(string guid) where TModel : FrogDataModel, new()
{
  /// <summary>
  /// A globally unique ID for this container
  /// </summary>
  public readonly string GUID = guid;

  /// <summary>
  /// Returns your most recently loaded save data or a fresh instance
  /// of it if none is found or an error occurs while loading
  /// </summary>
  public TModel GetModData()
  {
    if (FrogDataManager.TryGetModData<TModel>(GUID, out var data))
    {
      return data;
    }

    return new TModel();
  }

  /// <summary>
  /// Saves your data model to the current save file
  /// </summary>
  /// <remarks>Will print diagnostic output to the log file when returning false</remarks>
  /// <param name="data">The serializable model to save</param>
  /// <returns><b>TRUE</b> on success, <b>FALSE</b> on any failure</returns>
  public bool SaveModData(TModel data) =>
    FrogDataManager.TrySaveModData(GUID, data);
}
