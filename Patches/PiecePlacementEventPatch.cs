using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder.Patches
{
    [HarmonyPatch(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.ReceiveEvent))]
    static class PiecePlacementEventPatch
    {
        static void Postfix(PiecePlacementCursor __instance, InputEvent e)
        {
            if (__instance.Piece == null)
            {
                return;
            }

            if (e.Key == InputEvent.InputKey.Scoreboard && e.Valueb)
            {
                Reset(__instance.Piece.transform);
            }
        }

        static void Reset(Transform transform)
        {
            var quaternion = transform.rotation;

            float x = RoundToCardinal(quaternion.eulerAngles.x);
            float y = RoundToCardinal(quaternion.eulerAngles.y);
            float z = RoundToCardinal(quaternion.eulerAngles.z);

            transform.rotation = Quaternion.Euler(x, y, z);
        }

        static float RoundToCardinal(float eulerAngle)
        {
            return Mathf.Round(eulerAngle / 90f) * 90f;
        }
    }
}