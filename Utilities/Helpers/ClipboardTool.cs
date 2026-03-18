using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using Terraria;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class ClipboardTool
    {
        // Define the SDL2 method for setting clipboard text
        [DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetClipboardText", ExactSpelling = true)]
        private static extern int SDL_SetClipboardText([MarshalAs(UnmanagedType.LPStr)] string text);

        public static void Copy(string text)
        {
            try
            {
                // This calls the native SDL2 function used by the game engine itself
                _ = SDL_SetClipboardText(text);
            }
            catch
            {
                // Fallback for safety if SDL2 isn't found (rare in tModLoader environment)
                Terraria.Main.NewText("Clipboard copy failed: SDL2 not found.");
                AlienBloxUtility.AlienBloxLogger.Warn("No clippy.");
            }
        }

        public static void CopyZoomCorrectedColor(out string hexColor, bool copy = true)
        {
            hexColor = string.Empty;

            // 1. Get the render target
            RenderTarget2D target = Main.screenTarget;
            if (target == null)
            {
                Main.NewText("no target...");

                return;
            }

            // 2. Transform UI Mouse Position to Screen Target Space
            // We invert the matrix to go from "Screen" back to "World/Target" pixels
            Matrix invMatrix = Matrix.Invert(Main.GameViewMatrix.TransformationMatrix);
            Vector2 mousePos = new(Main.mouseX, Main.mouseY);
            Vector2 correctedPos = Vector2.Transform(mousePos, invMatrix);

            // 3. Clamp to ensure we don't sample outside the texture
            int x = (int)MathHelper.Clamp((int)correctedPos.X, 0, target.Width - 1);
            int y = (int)MathHelper.Clamp((int)correctedPos.Y, 0, target.Height - 1);

            // 4. Sample the pixel
            Color[] pixelData = new Color[1];
            Rectangle sourceRect = new(x, y, 1, 1);

            // Safety check for threaded environments (mostly for Draw calls)
            lock (target)
            {
                target.GetData(0, sourceRect, pixelData, 0, 1);
            }

            Color selectedColor = pixelData[0];

            string hex = $"#{selectedColor.R:X2}{selectedColor.G:X2}{selectedColor.B:X2}";

            if (copy)
            {
                // 5. Convert to Hex and Copy
                Copy(hex);

                // 6. Feedback for the user
                Main.NewText($"Copied: {hex}", selectedColor);
            }

            hexColor = hex;
        }
    }
}