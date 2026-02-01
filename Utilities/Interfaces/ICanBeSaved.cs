using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.Interfaces
{
    public interface ICanBeSaved
    {
        public void Save(TagCompound tag);

        public void Load(TagCompound tag);
    }
}