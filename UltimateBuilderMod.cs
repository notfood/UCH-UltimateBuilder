using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [BepInPlugin("notfood.UltimateBuilder", "UltimateBuilder", "1.3.1.0")]
    public class UltimateBuilderMod : BaseUnityPlugin
    {
        internal static bool enableRotationOverride = false;
        internal static bool enableColliderOverride = false;
        internal static bool enableGridOverride = false;

        void Awake()
        {
            new Harmony("notfood.UltimateBuilder").PatchAll();
        }
    }
}