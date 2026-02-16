namespace FrogDataLib.DataManagement;

/// <summary>
/// Automatically saves/refreshes your mod's data when the game is saved
/// or loaded. Designed for simple mods that do not perform any cleanup
/// </summary>
/// <typeparam name="TModel">Your data model type</typeparam>
public class FrogDataContainerSimple<TModel> : FrogDataContainer<TModel> where TModel : FrogDataModel, new()
{

  /// <summary>
  /// Initializes a new FrogDataContainerSimple with a GUID for
  /// tracking your save data. Automatically refreshes the data
  /// internally for you
  /// </summary>
  /// <param name="guid"></param>
  public FrogDataContainerSimple(string guid) : base(guid)
  {
    FrogDataManager.OnBeginSaving += OnSaveGame;
    FrogDataManager.OnLoadCompleted += OnLoadGame;
    FrogDataManager.OnSessionEnded += OnSessionEnd;
  }

  /// <summary>
  /// Your data model containing all the fields you defined for your save
  /// </summary>
  public TModel Data = new();

  private void OnSaveGame() =>
    SaveModData(Data);

  private void OnLoadGame() =>
    Data = GetModData();

  private void OnSessionEnd() =>
    Data = new();
}
