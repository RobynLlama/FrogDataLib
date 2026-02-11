namespace FrogDataLib.DataManagement;

/// <summary>
/// Automatically saves/refreshes your mod's data when the game is saved
/// or loaded. Designed for simple mods that do not perform any cleanup
/// </summary>
/// <typeparam name="TModel">Your data model type</typeparam>
/// <param name="guid">A globally unique ID</param>
public class FrogDataContainerSimple<TModel>(string guid) : FrogDataContainer<TModel>(guid) where TModel : FrogDataModel, new()
{

  public TModel Data = new();

  private void OnSaveGame() =>
    SaveModData(Data);

  private void OnLoadGame() =>
    Data = GetModData();
}
