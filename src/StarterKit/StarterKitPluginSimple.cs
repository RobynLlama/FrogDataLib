using BepInEx;
using BepInEx.Logging;
using FrogDataLib.DataManagement;

namespace StarterKit;

[BepInAutoPlugin]
public partial class StarterKitPluginSimple : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; }

  private void Awake()
  {
    Log = Logger;
    Log.LogInfo($"Plugin {Name} is loaded!");

    /*
      This is how you create the simple version of a data container
      it will handle all the callback logic for you so its a great
      choice if your mod has no other side-effects and just needs
      fresh data on each load!
      
      Note that we pass our GUID in here and use our ExampleData
      Type as the generic type argument when creating the container
    */
    var cont = new FrogDataContainerSimple<ExampleData>(Id);

    /*
      And that's it, the simple container will automatically listen
      to the 3 main callbacks and update your underlying data.

      Note: Do not cache your underlying data object because it will
      be wholly replaced inside the container. Use container.Data.SomeValue
      when accessing values to ensure you always have the freshest value
    */

    Log.LogInfo($"{cont.Data.Name} is my current ExampleData.Name value");
  }
}
