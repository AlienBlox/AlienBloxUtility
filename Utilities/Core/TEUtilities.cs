using System.Linq;
using Terraria.DataStructures;

namespace AlienBloxUtility.Utilities.Core
{
    /// <summary>
    /// The helpers for all things TE
    /// </summary>
    public static class TEUtilities
    {
        /// <summary>
        /// Returns a list of TE IDs
        /// </summary>
        /// <returns>The TEs</returns>
        public static int[] DynamicGetTE()
        {
            return [.. TileEntity.manager.EnumerateEntities().Keys];
        }

        public static TileEntity[] GetTEObjects()
        {
            return [.. TileEntity.manager.EnumerateEntities().Values];
        }

        public static TileEntity FromID(int id)
        {
            return GetTEObjects()[id];//GetTEObjects().FirstOrDefault(x => x.ID == id);
        }
    }
}