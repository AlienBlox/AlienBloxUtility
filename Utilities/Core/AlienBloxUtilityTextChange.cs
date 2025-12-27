using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
//Credits to Calamity Fable for the code.
namespace AlienBloxUtility.Utilities.Core
{
    public class AlienBloxUtilityTextChange : ModSystem
    {
        private static readonly Type UIModItemType = typeof(ModItem).Assembly.GetType("Terraria.ModLoader.UI.UIModItem");
        private static readonly MethodInfo InitializeModItemUIMethod = UIModItemType?.GetMethod("OnInitialize", BindingFlags.Instance | BindingFlags.Public);

        private static readonly PropertyInfo ModNameProperty = UIModItemType?.GetProperty("ModName", BindingFlags.Instance | BindingFlags.Public);
        private static readonly FieldInfo ModIconField = UIModItemType?.GetField("_modIcon", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo ModNameElement = UIModItemType?.GetField("_modName", BindingFlags.Instance | BindingFlags.NonPublic);

        public delegate void orig_OnInitialize(UIElement self);
        public delegate void hook_UpdateLifeRegen(orig_OnInitialize orig, UIElement self);
        public static Hook NameplateChanger;

        public static string UtilityModName;

        public override void Load()
        {
            if (InitializeModItemUIMethod == null || ModIconField == null || ModNameProperty == null)
            {
                AlienBloxUtility.Instance.Logger.Warn("ALIENBLOX'S UTILITY: BRAIN TUMOR ERROR, \n CAN'T BE CURED BECAUSE TECH AIN'T ADVANCED ENOUGH");
                return;
            }

            NameplateChanger = new Hook(InitializeModItemUIMethod, ChangeTextToFunnyOne);
            UtilityModName = Mod.Name;
        }

        public void ChangeTextToFunnyOne(orig_OnInitialize orig, UIElement element)
        {
            orig(element);

            if (element.GetType() != UIModItemType)
                return;

            var convertedSelf = Convert.ChangeType(element, UIModItemType);

            object potentialModName = ModNameProperty.GetValue(convertedSelf);
            if (potentialModName == null || potentialModName is not string modName || modName != UtilityModName)
                return;

            object potentiallyTheIcon = ModIconField.GetValue(convertedSelf);
            if (potentiallyTheIcon is UIImage modIconImage)
            {
                CustomNameBar addedDrawLogic = new CustomNameBar((UIText)ModNameElement.GetValue(convertedSelf));
                modIconImage.Append(addedDrawLogic);
                modIconImage.Color = Color.Transparent;
            }
        }
    }

    public class CustomNameBar : UIElement
    {
        public UIText ModName;

        public CustomNameBar(UIText nameUI)
        {
            Width.Set(80, 0f);
            Height.Set(80, 0f);

            ModName = nameUI;
        }

        public static Asset<Texture2D> ModIcon;

        public override void Update(GameTime gameTime)
        {
            if (ModName == null)
                return;

            if ((DateTime.Now.Month == 4 && DateTime.Now.Day == 1))
            {
                ModName.SetText("AlienBlox's Crappy Software With 1 Million Viruses v0.1");
            }

            ModName.TextColor = Color.Black;
            ModName.ShadowColor = Main.DiscoColor;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            ModIcon ??= ModContent.Request<Texture2D>("AlienBloxUtility/icon");

            CalculatedStyle dimensions = GetDimensions();

            Texture2D background = ModIcon.Value;
            Vector2 drawPos = dimensions.Position();
            spriteBatch.Draw(background, drawPos - Vector2.One * 0.5f, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}