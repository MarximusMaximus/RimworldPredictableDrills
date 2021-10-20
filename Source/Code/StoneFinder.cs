using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace PredictableDrills
{
    public static class StoneFinder
    {
        private static List<ThingDef> naturalRockDefs;

        /// <summary>
        /// Returns the base terrain type of the given cell
        /// </summary>
        /// <param name="map">Map.</param>
        /// <param name="cell">Cell.</param>
        public static TerrainDef GetBaseTerrainAt(this Map map, IntVec3 cell)
        {
            int idx = map.cellIndices.CellToIndex(cell);
            return map.terrainGrid.UnderTerrainAt(idx) ?? map.terrainGrid.TerrainAt(idx);
        }

        /// <summary>
        /// If this terrain is stone, returns the chunk associated with it.
        /// Otherwise returns null.
        /// </summary>
        /// <returns>The chunk.</returns>
        /// <param name="terrain">Terrain.</param>
        public static ThingDef GetChunk(this TerrainDef terrain)
        {
            if (naturalRockDefs == null)
            {
                naturalRockDefs = DefDatabase<ThingDef>.AllDefs
                                        .Where(d => d.IsNonResourceNaturalRock)
                                        .ToList();
            }

            return naturalRockDefs.Where(r => terrain.IsTerrainForRock(r))
                    .Select(r => r.building.mineableThing)
                    .FirstOrDefault();
        }

        /// <summary>
        /// If this terrain is a natural stone type for the given map, returns the chunk associated with it
        /// Otherwise returns null.
        /// </summary>
        /// <returns>The chunk.</returns>
        /// <param name="terrain">Terrain.</param>
        public static ThingDef GetMapChunk(this TerrainDef terrain, Map map)
        {
            return Find.World.NaturalRockTypesIn(map.Tile)
                    .Where(r => terrain.IsTerrainForRock(r))
                    .Select(r => r.building.mineableThing)
                    .FirstOrDefault();
        }


        /// <summary>
        /// Tests whether this terrain type is one of the associated terrain types for the given rock ThingDef
        /// </summary>
        /// <returns><c>true</c>, if terrain for rock was ised, <c>false</c> otherwise.</returns>
        /// <param name="terrain">Terrain.</param>
        /// <param name="rock">Rock.</param>
        public static bool IsTerrainForRock(this TerrainDef terrain, ThingDef rock)
        {
            return terrain == rock.building.leaveTerrain
                || terrain == rock.building.naturalTerrain
                || terrain == rock.building.leaveTerrain.smoothedTerrain
                || terrain == rock.building.naturalTerrain.smoothedTerrain;
        }
    }
}
