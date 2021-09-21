using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [BepInPlugin("notfood.UltimateBuilder", "UltimateBuilder", "1.0.0.0")]
    public class UltimateBuilderMod : BaseUnityPlugin
    {
        void Awake()
        {
            new Harmony("notfood.UltimateBuilder").PatchAll();

            Debug.LogWarning("[UltimateBuilder] Started");
        }
    }
}