using BepInEx;
using BepInEx.Logging;
using FrogDataLib.DataManagement;

namespace StarterKit;

[BepInAutoPlugin]
public partial class StarterKitPlugin : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; }

  private void Awake()
  {
    Log = Logger;
    Log.LogInfo($"Plugin {Name} is loaded!");

    /*
      This is how you manage a data container for your mod
      For simplicity's sake I will use inline anonymous actions
      to talk to FrogDataManager but I imagine it would be
      far better to use full methods and actually store your
      container in a field or property in a dedicated class
    */

    /*
      Note that we pass our GUID in here and use our ExampleData
      Type as the generic type argument when creating the container
    */
    var cont = new FrogDataContainer<ExampleData>(Id);

    //We create our default data for the save here
    ExampleData data = new();

    //Now we register with FrogDataManager for callbacks
    FrogDataManager.OnBeginSaving += () =>
    {
      Log.LogMessage("Saving my data");
      cont.SaveModData(data);
    };

    FrogDataManager.OnLoadCompleted += () =>
    {
      Log.LogMessage("Loading my data");
      data = cont.GetModData();
    };

    FrogDataManager.OnSessionEnded += () =>
    {
      /*
        Actually we don't need to do anything for such a simple
        example but this is where you'd do any cleanup because
        the user is returning to the main menu.
      */
      Log.LogMessage("Cleaning up my data :)");
    };
  }
}
