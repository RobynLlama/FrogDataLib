using FrogDataLib.DataManagement;
using HarmonyLib;
using YAPYAP;

namespace FrogDataLib.Patches;

internal static class SaveManagerPatches
{

  [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LoadSlot)), HarmonyPostfix]
  internal static void LoadSlotPostfix(int slot) =>
    FrogDataManager.LoadFromSlot(slot);

  [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.DeleteSlot)), HarmonyPostfix]
  internal static void DeleteSlotPostfix(int slot) =>
    FrogDataManager.DeleteSlot(slot);

  [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.WriteSlot)), HarmonyPostfix]
  internal static void WriteSlotPostfix(int slot) =>
    FrogDataManager.SaveToSlot(slot);
}
