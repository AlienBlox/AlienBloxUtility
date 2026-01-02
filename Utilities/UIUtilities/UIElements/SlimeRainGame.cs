using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    /// <summary>
    /// A game.
    /// </summary>
    public class SlimeRainGame : UIPanel
    {
        public List<NPCDisplay> Slimes;

        public UIText BottomText;

        public int Score = 0;

        public SlimeRainGame()
        {
            SetPadding(0);

            BottomText = this.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.SlimeGameText").Format(Score));
            BottomText.VAlign = 1;
            BottomText.Height.Set(0, .1f);

            VAlign = HAlign = .5f;
            Width.Percent = Height.Percent = 1;
            BackgroundColor = Main.DiscoColor;
            Slimes = [];

            ConHostRender.SetModal(true, true, this);
        }

        public override void RightDoubleClick(UIMouseEvent evt)
        {
            ConHostRender.SetModal(false, false, null);

            base.RightDoubleClick(evt);
        }

        public override void Update(GameTime gameTime)
        {
            BackgroundColor = Main.DiscoColor;
            BottomText.SetText(Language.GetText("Mods.AlienBloxUtility.UI.SlimeGameText").Format(Score));

            if (Main.rand.NextBool(10))
            {
                if (Main.rand.NextBool(100))
                {
                    var slime = new NPCDisplay(NPCID.RainbowSlime, Color.Wheat, true)
                    {
                        VAlign = 0,
                        HAlign = Main.rand.NextFloat(0, 1f)
                    };

                    slime.OnLeftClick += (_, _) =>
                    {
                        Score += 25;
                        slime.Remove();
                        SoundEngine.PlaySound(AlienBloxAudio.ExoDeathSound);
                    };

                    Slimes.Add(slime);

                    Append(slime);
                }
                else
                {
                    var slime = new NPCDisplay(NPCID.BlueSlime, new(Main.rand.Next(0, 255), Main.rand.Next(0, 255), Main.rand.Next(0, 255)))
                    {
                        VAlign = 0,
                        HAlign = Main.rand.NextFloat(0, 1f)
                    };

                    slime.OnLeftClick += (_, _) =>
                    {
                        Score += Main.rand.Next(1, 4);
                        slime.Remove();
                        SoundEngine.PlaySound(SoundID.NPCDeath1);
                    };

                    Slimes.Add(slime);

                    Append(slime);
                }
            }

            foreach (NPCDisplay display in Slimes)
            {
                display.VAlign += 0.01f;

                if (display.VAlign >= 1)
                {
                    display.Remove();
                }
            }

            base.Update(gameTime);
        }
    }

    /// <summary>
    /// Displays NPCs
    /// </summary>
    public class NPCDisplay : UIElement
    {
        public readonly int FrameCount;

        public readonly int ID;

        public readonly bool Rainbow;

        public readonly Asset<Texture2D> NPCTexture;

        public Color Color;

        private int currentFrame = 0;
        private readonly float frameSpeed = 0.1f; // Speed at which to cycle through frames (lower = faster)
        private float frameTimer = 0f;  // Timer for frame change

        public NPCDisplay(int id, Color color, bool rainbow = false)
        {
            ID = id;
            Color = color;
            Rainbow = rainbow;

            if (ID <= NPCID.Count)
            {
                Main.instance.LoadNPC(ID);

                FrameCount = Main.npcFrameCount[ID];
            }

            NPCTexture = TextureAssets.Npc[ID];

            Width.Set(NPCTexture.Width(), 0);
            Height.Set((NPCTexture.Height() / FrameCount) - 2, 0);

            /*
            var debugElem = new UIPanel();

            debugElem.Width.Set(0, 1);
            debugElem.Height.Set(0, 1);
            debugElem.VAlign = debugElem.HAlign = .5f;

            Append(debugElem);
            */
        }

        public override void Update(GameTime gameTime)
        {
            // Update the frame based on time
            frameTimer += 1f / 60f; // Increment by 1/60th of a second for each frame
            if (frameTimer >= frameSpeed)
            {
                currentFrame++;
                if (currentFrame >= FrameCount)
                    currentFrame = 0; // Loop back to the first frame
                frameTimer = 0f;
            }

            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            int high = NPCTexture.Value.Height / FrameCount;

            Rectangle sourceRectangle = new(0, currentFrame * high, NPCTexture.Value.Width, high);

            if (Rainbow)
                Color = Main.DiscoColor;

            // Draw the sprite with SpriteBatch
            spriteBatch.Draw(NPCTexture.Value, (GetDimensions().Position() + GetDimensions().Size() / 2) - new Vector2(NPCTexture.Value.Width / 2, (NPCTexture.Value.Height / FrameCount) / 2), sourceRectangle, Color);

            base.DrawSelf(spriteBatch);
        }
    }
}