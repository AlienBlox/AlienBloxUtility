using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class DebugVisionShader(string passName) : ScreenShaderData(passName)
    {
        public static bool useEffect;

        public override void Apply()
        {
            UseTargetPosition(Main.LocalPlayer.Center);
            base.Apply();
        }

        public override void Update(GameTime gameTime)
        {
            useEffect = true;

            Use();

            base.Update(gameTime);
        }

        public static void Use()
        {
            if (useEffect)
                Filters.Scene.Activate("AlienBloxUtility:HighContrastShader");
            else
                Filters.Scene["AlienBloxUtility:HighContrastShader"].Deactivate([]);
        }
    }
}