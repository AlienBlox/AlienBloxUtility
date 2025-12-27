using AlienBloxUtility.Common.Exceptions;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class DocumentationStorage : ModSystem
    {
        public static List<DocumentationEntry> Entries;

        public class DocumentationEntry
        {
            public string LocalizationLocation;

            public string mod;

            public string entryName;

            public bool lua;

            public bool csharp;

            public bool core;

            public bool js;

            public string Title => Language.GetOrRegister(LocalizationLocation + ".Title", () => { return "cat"; }).Value;
            public string Description => Language.GetOrRegister(LocalizationLocation + ".Description", () => { return "cat"; }).Value;

            public string FullName => $"{mod}:{entryName}";
        }

        public static void RegisterEntry(Mod mod, string entryname, string localization, bool lua, bool csharp, bool core, bool js)
        {
            var entry = new DocumentationEntry()
            {
                LocalizationLocation = localization,
                lua = lua,
                csharp = csharp,
                core = core,
                js = js,
                mod = mod.Name,
                entryName = entryname
            };

            foreach (var obj in Entries)
            {
                if (obj.FullName == entry.FullName)
                {
                    throw new DocumentationException();
                }
            }

            Entries.Add(entry);
        }

        public static UIPanel GetDocumentationPanel(string fullName)
        {
            foreach (var obj in Entries)
            {
                if (obj.FullName == fullName)
                {
                    UIPanel titleBar = new();

                    titleBar.Height.Set(0, .1f);
                    titleBar.Width.Set(0, 1);
                    titleBar.HAlign = .5f;
                    titleBar.VAlign = 0f;
                    titleBar.InsertText(obj.Title);

                    UIElement backer = new();

                    backer.Width.Set(0, 1);
                    backer.Height.Set(0, .9f);
                    backer.VAlign = 1;
                    backer.HAlign = .5f;

                    UIText t = backer.InsertText(obj.Description);

                    t.DynamicallyScaleDownToWidth = true;
                    t.IsWrapped = true;

                    UIPanel panel = new();
                    panel.Width.Set(0, 1f);
                    panel.Height.Set(300, 0);
                    panel.SetPadding(0);

                    panel.Append(backer);
                    panel.Append(titleBar);

                    return panel;
                }
            }

            return null;
        }

        public static UIPanel GetDocumentationPanel(DocumentationEntry entry)
        {
            return GetDocumentationPanel(entry.FullName);
        }

        public override void Load()
        {
            Entries = [];

            //RegisterEntry(Mod, "ExampleEntry", "Mods.AlienBloxUtility.Documents.Test", true, true, true, true);
            //RegisterEntry(Mod, "ExampleLua", "Mods.AlienBloxUtility.Documents.Lua", true, false, false, false);
            RegisterEntry(Mod, "AlienBloxUtility", "Mods.AlienBloxUtility.Documents.AlienBloxUtility", true, true, true, true);
            RegisterEntry(Mod, "Console", "Mods.AlienBloxUtility.Documents.Console", true, false, true, true);
            RegisterEntry(Mod, "Decompiler", "Mods.AlienBloxUtility.Documents.Decompiler", false, true, true, false);
            RegisterEntry(Mod, "StatsBar", "Mods.AlienBloxUtility.Documents.StatsBar", false, false, true, false);
            RegisterEntry(Mod, "JavaScript", "Mods.AlienBloxUtility.Documents.JavaScript", false, false, true, true);
            RegisterEntry(Mod, "Lua", "Mods.AlienBloxUtility.Documents.Lua", true, false, true, false);
        }

        public override void Unload()
        {
            Entries = null;
        }
    }
}