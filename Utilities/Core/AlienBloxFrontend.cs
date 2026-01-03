using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System.Collections;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.Core
{
    /// <summary>
    /// Used for the frontend in AlienBlox's Utility
    /// </summary>
    public static class AlienBloxFrontend
    {
        /// <summary>
        /// Creates a new UIList quickly.
        /// </summary>
        /// <param name="elem">the element to insert into.</param>
        /// <returns>The UI list itself</returns>
        public static UIList InsertList(this UIElement elem)
        {
            var list = new UIList();
            var scroll = new FixedUIScrollbar(UserInterface.ActiveInstance);

            scroll.Height.Set(0, 1);

            list.VAlign = list.HAlign = .5f;
            list.Width = list.Height = new(0, 1);

            list.Append(scroll);
            list.SetScrollbar(scroll);
            elem.Append(list);

            return list;
        }

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

                Panels.Add(panel);
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

                Panels.Add(panel);
            }

            return [.. Panels];
        }
    }
}