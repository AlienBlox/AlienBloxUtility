using System;
using System.Xml;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class LuaMetadata
    {
        public string Author { get; set; }
        public string Origin { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }

    public static class LuaMetadataParser
    {
        public static LuaMetadata ParseMetadata(string luaCode)
        {
            var lines = luaCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var xmlLines = new System.Collections.Generic.List<string>();
            bool insideMeta = false;

            // Extract only the lines inside <meta> block and remove "--" comment prefix
            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("--"))
                    trimmed = trimmed.Substring(2).Trim();
                else
                    continue; // skip non-comment lines

                if (trimmed.StartsWith("<meta>"))
                    insideMeta = true;

                if (insideMeta)
                    xmlLines.Add(trimmed);

                if (trimmed.StartsWith("</meta>"))
                    break;
            }

            if (xmlLines.Count == 0)
                return null; // no metadata found

            var xmlString = string.Join(Environment.NewLine, xmlLines);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            // Create metadata object
            var meta = new LuaMetadata
            {
                Author = xmlDoc.SelectSingleNode("/meta/author")?.InnerText,
                Origin = xmlDoc.SelectSingleNode("/meta/origin")?.InnerText,
                Version = xmlDoc.SelectSingleNode("/meta/version")?.InnerText,
                Description = xmlDoc.SelectSingleNode("/meta/description")?.InnerText
            };

            return meta;
        }
    }
}