using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch(typeof(GameState), nameof(GameState.Update))]
    static class GameStateUpdatePatch
    {
        static void Prefix()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                ToggleCustomRotation();
            }
        }

        static void ToggleCustomRotation()
        {
            UltimateBuilderMod.enableCustomRotation = !UltimateBuilderMod.enableCustomRotation;

            string message = UltimateBuilderMod.enableCustomRotation ? "Enabled" : "Disabled";
            UserMessageManager.Instance.UserMessage($"Custom Rotations {message}");
        }
    }
}