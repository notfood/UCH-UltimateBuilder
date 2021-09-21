using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch]
    static class RemoveRotationLimitsPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Placeable), nameof(Placeable.Place), new[] {typeof(int), typeof(bool), typeof(bool)});
            yield return AccessTools.Method(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.SetPiece));
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> e)
        {
            var target = AccessTools.PropertySetter(typeof(Transform), nameof(Transform.rotation));

            var insts = e.ToList();

            int start, end;

            end = insts.FindIndex(inst => inst.opcode == OpCodes.Callvirt && object.Equals(inst.operand, target)) + 1;
            start = insts.FindLastIndex(end, inst => inst.opcode == OpCodes.Stloc_0 || inst.opcode == OpCodes.Stloc_S) + 1;
            insts.RemoveRange(start, end-start);

            return insts;
        }
    }
}