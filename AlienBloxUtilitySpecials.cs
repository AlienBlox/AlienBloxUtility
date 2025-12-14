using AlienBloxUtility.Utilities.Core;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility
{
    public static class AlienBloxUtilitySpecials
    {
        /// <summary>
        /// Quickly sets the margin of an UIElement
        /// </summary>
        /// <param name="element">The element to set.</param>
        /// <param name="margin">The margin</param>
        /// <returns>The margin</returns>
        public static float SetMargin(this UIElement element, float margin)
        {
            return element.MarginBottom = element.MarginLeft = element.MarginRight = element.MarginTop = margin;
        }

        /// <summary>
        /// Quickly inserts a text into an UI element
        /// </summary>
        /// <param name="element">The element to insert into</param>
        /// <param name="text">The text</param>
        /// <param name="textScale">The text scale</param>
        /// <param name="large">Should the text be large</param>
        /// <returns>The created text.</returns>
        public static UIText InsertText(this UIElement element, string text, float textScale = 1, bool large = false)
        {
            UIText textElement = new(text, textScale, large);

            textElement.Width.Set(0, 1);
            textElement.Height.Set(0, 1);
            textElement.VAlign = textElement.HAlign = 0.5f;
            
            element.Append(textElement);

            return textElement;
        }

        /// <summary>
        /// Quickly inserts a text into an UI element
        /// </summary>
        /// <param name="element">The element to insert into</param>
        /// <param name="text">The localized text</param>
        /// <param name="textScale">The text scale</param>
        /// <param name="large">Should the text be large</param>
        /// <returns>The created text.</returns>
        public static UIText InsertText(this UIElement element, LocalizedText text, float textScale = 1, bool large = false)
        {
            UIText textElement = new(text, textScale, large);

            textElement.Width.Set(0, 1);
            textElement.Height.Set(0, 1);
            textElement.VAlign = textElement.HAlign = 0.5f;

            element.Append(textElement);

            return textElement;
        }

        /// <summary>
        /// Turns a string array into a text for writing files with.
        /// </summary>
        /// <param name="sArray">The array to query</param>
        /// <returns>The entire string</returns>
        public static string MakeString(this string[] sArray)
        {
            string EntireString = string.Empty;

            foreach (string s in sArray)
            {
                EntireString += s + "\n";
            }

            return EntireString;
        }

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