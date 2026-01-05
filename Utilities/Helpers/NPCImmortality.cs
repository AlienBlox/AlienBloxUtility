using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class NPCImmortality : GlobalNPC
    {
        /// <summary>
        /// Should town NPCs be immortal (true if enabled)
        /// </summary>
        public static bool GlobalImmortality;

        public override void PostAI(NPC npc)
        {
            if (npc.isLikeATownNPC)
            {
                if (npc.dontTakeDamage != GlobalImmortality)
                {
                    npc.dontTakeDamage = GlobalImmortality;
                    npc.netUpdate = true;
                }
            }
        }
    }
}