using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch]
    static class PerformRotationPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.PerformLeftRotation));
            yield return AccessTools.Method(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.PerformRightRotation));
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> e)
        {
            var target = AccessTools.Method(typeof(Transform), nameof(Transform.Rotate), new [] {typeof(float), typeof(float), typeof(float)});
            var replace = AccessTools.Method(typeof(PerformRotationPatch), nameof(RotateWithModifiers));

            foreach(var inst in e)
            {
                if (inst.opcode == OpCodes.Callvirt && object.Equals(inst.operand, target))
                {
                    yield return new CodeInstruction(OpCodes.Call, replace);
                }
                else
                {
                    yield return inst;
                }
            }
        }
        static void RotateWithModifiers(Transform transform, float x, float y, float z)
        {
            z /= 2;

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                z /= 3;
            }

            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                z /= 5;
            }

            transform.Rotate(x, y, z);
        }
    }
}