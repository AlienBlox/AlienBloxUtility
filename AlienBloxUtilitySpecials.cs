using AlienBloxUtility.Utilities.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;
using static System.Net.Mime.MediaTypeNames;

namespace AlienBloxUtility
{
    public static class AlienBloxUtilitySpecials
    {
        /// <summary>
        /// Removes any characters from the input string that are invalid for file names.
        /// </summary>
        /// <param name="input">The input string to sanitize.</param>
        /// <returns>A safe string that can be used as a file name.</returns>
        public static string SanitizeFileName(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(input.Where(ch => !invalidChars.Contains(ch)).ToArray());

            // Optionally, trim whitespace
            return sanitized.Trim();
        }

        public static UtilityPlayer AlienBloxUtility(this Player player)
        {
            return player.GetModPlayer<UtilityPlayer>();
        }

        /// <summary>
        /// Does the base locking action for the UI like setting mouseInterface
        /// </summary>
        /// <param name="Element">The element to do for</param>
        /// <param name="textToDisplay">What text should be displayed when you hover over the UI</param>
        /// <param name="ApplyToChildren">Should this be applied to children elements</param>
        public static void SetUIBase(this UIElement Element, string textToDisplay, bool ApplyToChildren = false)
        {
            if (Element.IsMouseHovering)
            {
                Main.hoverItemName = textToDisplay;
            }

            if (Element.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (Element.IsMouseHovering)
            {
                PlayerInput.LockVanillaMouseScroll("AlienBloxUtility/ScrollListA"); // The passed in string can be anything.
            }
            
            if (ApplyToChildren)
            {
                foreach (UIElement elementSub in Element.Children)
                {
                    if (elementSub.ContainsPoint(Main.MouseScreen))
                    {
                        Main.LocalPlayer.mouseInterface = true;
                    }

                    if (elementSub.IsMouseHovering)
                    {
                        PlayerInput.LockVanillaMouseScroll("AlienBloxUtility/ScrollListB"); // The passed in string can be anything.
                    }
                }
            }
        }
    }
}