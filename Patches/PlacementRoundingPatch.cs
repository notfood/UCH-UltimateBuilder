using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace UltimateBuilder
{
    [HarmonyPatch]
    static class PlacementRoundingPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.SetPiece));
            yield return AccessTools.Method(typeof(PiecePlacementCursor), nameof(PiecePlacementCursor.FixedUpdate));
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> e)
        {
            var target = AccessTools.Field(typeof(Cursor), nameof(Cursor.gridPosition));
            var round = AccessTools.Method(typeof(Mathf), nameof(Mathf.Round));
            var replace = AccessTools.Method(typeof(PlacementRoundingPatch), nameof(Round));

            int count = -1;

            foreach(var inst in e)
            {
                if (object.Equals(inst.operand, target))
                {
                    count = 0;
                }

                if (count >= 0 && object.Equals(inst.operand, round))
                {
                    inst.operand = replace;
                    count++;
                    if (count >= 2)
                    {
                        count = -1;
                    }
                }
                
                yield return inst;
            }
            
        }

        static float Round(float f)
        {
            if (UltimateBuilderMod.enableGridOverride)
            {
                float accuracy = 100f;
                return Mathf.Round(f * accuracy) / accuracy;
            }
            else
            {
                return Mathf.Round(f);
            }
        }
    }
}