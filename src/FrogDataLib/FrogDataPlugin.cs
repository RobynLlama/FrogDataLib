using BepInEx;
using BepInEx.Logging;
using FrogDataLib.DataManagement;

namespace FrogDataLib;

// Here are some basic resources on code style and naming conventions to help
// you in your first CSharp plugin!
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
// https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces

// This BepInAutoPlugin attribute comes from the Hamunii.BepInEx.AutoPlugin
// NuGet package, and it will generate the BepInPlugin attribute for you!
// For more info, see https://github.com/Hamunii/BepInEx.AutoPlugin
[BepInAutoPlugin]
public partial class FrogDataPlugin : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; }

  private void Awake()
  {
    Log = Logger;
    Log.LogInfo($"Plugin {Name} is loaded!");

    if (!FrogDataManager.PersistentPath.Exists)
    {
      Log.LogMessage("Creating persistent data path for FrogDataLib");
      FrogDataManager.PersistentPath.Create();
    }
  }
}
