using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System.Collections;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;

namespace AlienBloxUtility.Utilities.Core
{
    /// <summary>
    /// Used for the frontend in AlienBlox's Utility
    /// </summary>
    public static class AlienBloxFrontend
    {
        /// <summary>
        /// Turns a list of objects into UI panels
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="objects">The objects</param>
        /// <returns>The panels</returns>
        public static UIPanel[] EnumerateToMenu(this IEnumerable objects)
        {
            List<UIPanel> Panels = [];

            foreach (var obj in objects)
            {
                var panel = new UIPanel();

                panel.Width.Set(0, 1);
                panel.Height.Set(30, 0);
                panel.OnMouseOver += ConHostSystem.HoverTick;
                panel.OnMouseOut += ConHostSystem.Unhover;
            }

            return [.. Panels];
        }

        /// <summary>
        /// Turns a list of objects into UI panels
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="objects">The objects</param>
        /// <returns>The panels</returns>
        public static UIPanel[] EnumerateToMenu<T>(this IEnumerable<T> objects)
        {
            List<UIPanel> Panels = [];

            foreach (var obj in objects)
            {
                var panel = new UIPanel();

                panel.Width.Set(0, 1);
                panel.Height.Set(30, 0);
                panel.OnMouseOver += ConHostSystem.HoverTick;
                panel.OnMouseOut += ConHostSystem.Unhover;
            }

            return [.. Panels];
        }
    }
}