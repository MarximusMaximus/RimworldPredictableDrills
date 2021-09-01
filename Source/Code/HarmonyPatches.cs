using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PredictableDrills
{
    [StaticConstructorOnStartup]
    class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("pausbrak.predictabledeepdrills");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(DeepDrillUtility))]
    [HarmonyPatch("GetBaseResource", new Type[] { typeof(Map), typeof(IntVec3)})]
    static class Patch_PathFinder_FindPath
    {
        static ThingDef Postfix(ThingDef resource, Map map, IntVec3 cell)
        {
            // Don't drop stones if we wouldn't be dropping them anyway
            if (resource == null) return null;

            // Default to whatever vanilla generated if we can't find a proper chunk type
            return cell.GetTerrain(map).GetMapChunk(map) ?? resource;
        }
    }
}
