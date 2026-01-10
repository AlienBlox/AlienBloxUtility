using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.Tools
{
    public class TestTool : DebugTool
    {
        public override void OnToolUse(Player user, bool sudo)
        {
            Console.WriteLine("tool usage");
            
            if (Main.netMode != NetmodeID.Server)
            {
                ConHostRender.Write("tool usage");
            }
        }
    }
}