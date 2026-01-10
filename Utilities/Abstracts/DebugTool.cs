using AlienBloxUtility.Common.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Abstracts
{
    /// <summary>
    /// A base tool in AlienBlox's Utility
    /// </summary>
    public abstract class DebugTool : ILoadable
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static List<DebugTool> Tools;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        /// <summary>
        /// The connected mod of this tool
        /// </summary>
        public Mod Mod { get; private set; }

        /// <summary>
        /// The name of this tool.
        /// </summary>
        public string Name => GetType().Name;

        /// <summary>
        /// The ID of this tool.
        /// </summary>
        public int ToolID;

        /// <summary>
        /// Called on server, singleplayer and clients when a debug tool is used
        /// </summary>
        /// <param name="user">The player that used the tool</param>
        /// <param name="sudo">Was the tool used in Sudo mode.</param>
        public virtual void OnToolUse(Player user, bool sudo)
        {

        }

        /// <summary>
        /// Use a tool
        /// </summary>
        /// <param name="tool">the name of the tool to use</param>
        public static void UseTool(string tool, int player, bool sudo)
        {
            try
            {
                using MemoryStream ms = new();
                using BinaryWriter bw = new(ms);

                bw.Write(tool);
                bw.Write(player);
                bw.Write(sudo);

                AlienBloxUtility.SendAlienBloxPacket("OnToolUsePacket", ms.ToArray());

                foreach (var item in Tools)
                {
                    if (item.Name == tool)
                    {
                        item.OnToolUse(Main.player[player], sudo);
                    }
                }
            }
            catch
            {

            }
        }

        public void Load(Mod mod)
        {
            Tools ??= [];

            foreach (var tool in Tools)
            {
                if (Name == tool.Name)
                {
                    throw new ConflictException();
                }
            }

            Tools.Add(this);

            Mod = mod;
            ToolID = Tools.Count;
        }

        public void Unload()
        {
            Tools.Remove(this);
        }
    }
}