using AlienBloxUtility.Common.Exceptions;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class CmdHelperSystem : ModSystem
    {
        public abstract class CommandHelper : ILoadable
        {
            public int CommandID;

            public virtual string CommandName { get; set; }

            public virtual string FriendlyDescription { get; set; }

            public virtual void OnLoad()
            {
                
            }

            public virtual void OnUnload()
            {

            }

            /// <summary>
            /// Launches the command.
            /// </summary>
            /// <param name="Conhost">The Console menu to print things to.</param>
            /// <param name="Params">The params of the command</param>
            public virtual void LaunchCommand(ConHostSystem Conhost, params string[] Params)
            {
                
            }

            public bool TryParse(string content, ConHostSystem ConHost)
            {
                // Split input by spaces
                string[] parts = content.Split(' ');

                if (parts.Length == 0)
                {
                    ConHost.AddConsoleText("No command entered.");
                    return false;
                }

                // First word is the command
                string command = parts[0].ToLower();

                // Remaining words are parameters
                string[] parameters = new string[parts.Length - 1];
                Array.Copy(parts, 1, parameters, 0, parameters.Length);

                if (command.Equals(CommandName, StringComparison.CurrentCultureIgnoreCase))
                {
                    LaunchCommand(ConHost, parameters);

                    return true;
                }

                return false;
            }

            public void Load(Mod mod)
            {
                if (FriendlyDescription == default)
                {
                    FriendlyDescription = Language.GetOrRegister($"Mods.AlienBloxUtility.Commands.{GetType().Name}").Value;
                }

                AddCommand(this);
                OnLoad();
            }

            public void Unload()
            {
                OnUnload();
            }
        }

        private static List<CommandHelper> CmdHelpers;

        public override void Load()
        {
            CmdHelpers ??= [];
        }

        public override void Unload()
        {
            CmdHelpers = null;
        }

        public static void AddCommand(CommandHelper command)
        {
            CmdHelpers ??= [];

            foreach (CommandHelper helper in CmdHelpers)
            {
                if (helper != null && helper.CommandName == command.CommandName)
                {
                    throw new CommandConflictException();
                }
            }

            CmdHelpers?.Add(command);

            if (CmdHelpers != null)
            {
                command.CommandID = CmdHelpers.Count;
            }
        }

        /// <summary>
        /// Calls a command.
        /// </summary>
        /// <param name="content">The content to call</param>
        /// <param name="conHost">The Console to call from.</param>
        public static void Call(string content, ConHostSystem conHost)
        {
            conHost.AddConsoleText(content);

            foreach (CommandHelper helper in CmdHelpers)
            {
                if (helper.TryParse(content, conHost))
                {
                    conHost.AddConsoleText("Command ran.");

                    return;
                }
            }

            //conHost.AddConsoleText("Command failed!");
        }

        public static string[] GetCmdNames()
        {
            List<string> names = [];

            if (CmdHelpers != null)
            {
                foreach (CommandHelper cmd in CmdHelpers)
                {
                    names.Add(cmd.CommandName);
                }
            }

            return [.. names];
        }

        public static string[] GetCmdFriendlyDescription()
        {
            List<string> names = [];

            if (CmdHelpers != null)
            {
                foreach (CommandHelper cmd in CmdHelpers)
                {
                    names.Add(cmd.FriendlyDescription);
                }
            }

            return [.. names];
        }

        /// <summary>
        /// Description helper...
        /// </summary>
        /// <param name="cmdName">we</param>
        /// <returns>we</returns>
        public static string DescFromCommandName(string cmdName)
        {
            foreach (CommandHelper cmd in CmdHelpers)
            {
                if (cmd.CommandName == cmdName)
                {
                    return cmd.FriendlyDescription;
                }
            }

            return "";
        }
    }
}