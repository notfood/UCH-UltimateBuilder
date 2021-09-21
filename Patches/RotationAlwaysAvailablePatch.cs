using HarmonyLib;

namespace UltimateBuilder
{
    [HarmonyPatch(typeof(Placeable), nameof(Placeable.Awake))]
    static class RotationAlwaysAvailablePatch
    {
        static void Postfix(Placeable __instance)
        {
            if (__instance is SwingingAxe)
            {
                return;
            }
            if (__instance.OrientationAlt == Placeable.OrientMode.NONE && __instance.Orientation != Placeable.OrientMode.ROTATE)
            {
                __instance.OrientationAlt = Placeable.OrientMode.ROTATE;
            }
        }
    }
}