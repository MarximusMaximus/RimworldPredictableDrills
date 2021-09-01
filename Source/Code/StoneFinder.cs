using System;
using System.Linq;
using Verse;

namespace PredictableDrills
{
    public static class StoneFinder
    {
        /// <summary>
        /// If this terrain is a natural stone type for the given map, returns the chunk associated with it
        /// Otherwise returns null.
        /// </summary>
        /// <returns>The chunk.</returns>
        /// <param name="terrain">Terrain.</param>
        public static ThingDef GetMapChunk(this TerrainDef terrain, Map map)
        {
            return Find.World.NaturalRockTypesIn(map.Tile)
                    .FirstOrDefault(r =>
                    {
                        return terrain == r.building.leaveTerrain
                            || terrain == r.building.naturalTerrain
                            || terrain == r.building.leaveTerrain.smoothedTerrain
                            || terrain == r.building.naturalTerrain.smoothedTerrain;
                    });
        }
    }
}
