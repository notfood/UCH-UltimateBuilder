using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch(typeof(GameState), nameof(GameState.Update))]
    static class GameStateUpdatePatch
    {
        static void Prefix()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                ToggleRotationOverride();
            }
            else if(Input.GetKeyDown(KeyCode.F9))
            {
                ToggleColliderOverride();
            }
            else if(Input.GetKeyDown(KeyCode.F10))
            {
                ToggleGridOverride();
            }
            else if(Input.GetKeyDown(KeyCode.F11))
            {
                DisableAll();
            }
        }

        static void ToggleRotationOverride()
        {
            UltimateBuilderMod.enableRotationOverride = !UltimateBuilderMod.enableRotationOverride;

            NotifyChanged("Rotation Override", UltimateBuilderMod.enableRotationOverride);
        }

        static void ToggleColliderOverride()
        {
            UltimateBuilderMod.enableColliderOverride = !UltimateBuilderMod.enableColliderOverride;

            NotifyChanged("Collision Override", UltimateBuilderMod.enableColliderOverride);
        }

        static void ToggleGridOverride()
        {
            UltimateBuilderMod.enableGridOverride = !UltimateBuilderMod.enableGridOverride;

            NotifyChanged("Grid Override", UltimateBuilderMod.enableGridOverride);
        }

        static void DisableAll()
        {
            UltimateBuilderMod.enableRotationOverride = false;
            UltimateBuilderMod.enableColliderOverride = false;
            UltimateBuilderMod.enableGridOverride = false;

            NotifyChanged("All Overrides", false);
        }

        static void NotifyChanged(string name, bool value)
        {
            UserMessageManager.Instance.UserMessage($"{name} {(value ? "Enabled" : "Disabled")}");
        }
    }
}