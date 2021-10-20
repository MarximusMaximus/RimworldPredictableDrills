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
            if (resource == null && !PredictableDrill.Settings.alwaysDropStone) return null;

            TerrainDef naturalTerrain = map.GetBaseTerrainAt(cell);

            ThingDef chunk = PredictableDrill.Settings.dropNonMapStone ?
                    naturalTerrain.GetChunk() : naturalTerrain.GetMapChunk(map);

            // Default to whatever vanilla generated if we can't find a chunk type for the given terrain
            return chunk ?? resource;
        }
    }
}
