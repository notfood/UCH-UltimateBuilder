using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch(typeof(QuickSaver), nameof(QuickSaver.RestoreSaveables))]
    static class RestoreSaveablesPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> e)
        {
            var target = AccessTools.PropertySetter(typeof(Transform), nameof(Transform.rotation));

            List <CodeInstruction> insts = e.ToList();

            int start, end;

            end = insts.FindIndex(inst => inst.opcode == OpCodes.Callvirt && object.Equals(inst.operand, target));
            end = insts.FindLastIndex(end, inst => inst.opcode == OpCodes.Stfld) + 1;
            start = insts.FindLastIndex(end, inst => inst.opcode == OpCodes.Stloc_S) + 1;
            insts.RemoveRange(start, end-start);

            end = insts.FindIndex(end, inst => inst.opcode == OpCodes.Callvirt && object.Equals(inst.operand, target));
            end = insts.FindLastIndex(end, inst => inst.opcode == OpCodes.Stfld) + 1;
            start = insts.FindLastIndex(end, inst => inst.opcode == OpCodes.Stloc_S) + 1;
            insts.RemoveRange(start, end-start);

            return insts;
        }
    }
}