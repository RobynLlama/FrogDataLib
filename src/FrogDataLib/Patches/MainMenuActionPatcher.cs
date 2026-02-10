using System;
using FrogDataLib.DataManagement;
using HarmonyLib;
using YAPYAP;

namespace FrogDataLib.Patches;

internal static class MainMenuActionPatcher
{

  internal static Action? orig;

  [HarmonyPatch(typeof(UISettings), nameof(UISettings.SetMainMenuAction)), HarmonyPrefix]
  internal static void Patch()
  {
    FrogDataPlugin.Log.LogMessage("Taking over UISettings._mainMenuAction");
    orig ??= UIManager.Instance.uiSettings._mainMenuAction;
    UIManager.Instance.uiSettings._mainMenuAction = MenuActionListener;
  }

  internal static void MenuActionListener()
  {
    //Invoke the original action
    orig?.Invoke();

    //Inform clients the session has ended
    FrogDataManager.SessionEnded();
  }
}
