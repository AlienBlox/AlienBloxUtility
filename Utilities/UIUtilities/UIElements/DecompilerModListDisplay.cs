using AlienBloxTools;
using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class DecompilerModListDisplay : UIPanel
    {
        public UIText text;
        public string ModConnected;
        public Mod Mod { get; private set; }

        /// <summary>
        /// Makes a new mod decompiler stat display
        /// </summary>
        /// <param name="ModName">The mod name to query</param>
        /// <exception cref="NullReferenceException">If a mod can't be found, this is shown</exception>
        public DecompilerModListDisplay(string ModName)
        {
            if (!ModLoader.TryGetMod(ModName, out Mod M))
            {
                throw new NullReferenceException("You can't decompile a mod that doesn't exist lol.");
            }

            Mod = M;
            ModConnected = ModName;
        }

        public override void OnInitialize()
        {
            text = new(Mod.DisplayName);
            text.Width.Percent = 1f;
            text.Height.Percent = 1f;

            SetPadding(5);

            Width.Set(0, 1f);
            Height.Set(30, 0);

            Append(text);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            TModInspector.DecompileModThreadSafe(ModConnected);

            base.LeftClick(evt);
        }
    }
}