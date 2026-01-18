using Terraria.Audio;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class ScreamJumpscare
    {
        public static bool ScreamVisual { get; set; }

        public static void DoScream()
        {
            SoundEngine.PlaySound(AlienBloxUtility.ScreamOfHorror);

            ScreamVisual = true;
        }
    }
}