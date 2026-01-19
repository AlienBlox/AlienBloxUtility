using Terraria;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class WorldfileSaver
    {
        public static void SaveWorldFile()
        {
            var world = Main.ActiveWorldFileData;

            if (!world.IsCloudSave)
                world.CopyToLocal(world.Name + " (Autodump)");
            else
            {
                world.MoveToLocal();
                world.CopyToLocal(world.Name + " (Autodump)");
                world.MoveToCloud();
            }
        }
    }
}