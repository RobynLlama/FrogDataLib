using System.Linq;
using BepInEx;
using BepInEx.Logging;
using FrogDataLib.DataManagement;
using FrogDataLib.Patches;
using HarmonyLib;

namespace FrogDataLib;

/// <summary>
/// Intentionally left blank
/// </summary>
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

    Harmony patcher = new(Id);
    patcher.PatchAll(typeof(MainMenuActionPatcher));
    patcher.PatchAll(typeof(SaveManagerPatches));

    Log.LogInfo($"Patch count is {patcher.GetPatchedMethods().Count()}");
  }
}
