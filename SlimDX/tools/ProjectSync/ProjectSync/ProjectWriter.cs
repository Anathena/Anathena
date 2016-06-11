using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ProjectSync
{
    enum ItemType
    {
        Filters,
        Includes,
        Sources,
        Resources,
        Other
    }

    class ProjectWriter
    {
        static readonly XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        string path;
        XDocument document;
        Dictionary<ItemType, XElement> groups = new Dictionary<ItemType, XElement>();

        public ProjectWriter(string path)
        {
            this.path = path;
            document = XDocument.Load(path);
            groups[ItemType.Filters] = FindItemGroup("Filter");
            groups[ItemType.Includes] = FindItemGroup("ClInclude");
            groups[ItemType.Sources] = FindItemGroup("ClCompile");
            groups[ItemType.Resources] = FindItemGroup("EmbeddedResource");
            groups[ItemType.Other] = FindItemGroup("None");
        }

        public void Save()
        {
            document.Save(path);
        }

        public void GetFilterIdentifiers(Dictionary<string, string> filters)
        {
            foreach (var item in groups[ItemType.Filters].Elements(ns + "Filter"))
            {
                string name = (string)item.Attribute("Include");
                if (filters.ContainsKey(name))
                    filters[name] = item.Element(ns + "UniqueIdentifier").Value;
            }

            foreach (var key in filters.Keys.ToList())
            {
                if (string.IsNullOrEmpty(filters[key]))
                    filters[key] = "{" + Guid.NewGuid().ToString() + "}";
            }
        }

        public void UpdateItems(ItemType type, Dictionary<string, string> items, bool writeFilter)
        {
            var element = groups[type];
            if (element == null)
                return;

            element.RemoveAll();
            foreach (var pair in items)
            {
                XElement ne = null;
                var include = new XAttribute("Include", pair.Key);
                var filter = new XElement(ns + "Filter", pair.Value);

                if (type == ItemType.Includes)
                {
                    ne = new XElement(ns + "ClInclude", include);
                    if (writeFilter && !string.IsNullOrEmpty(pair.Value))
                        ne.Add(filter);
                }
                else if (type == ItemType.Sources)
                {
                    ne = new XElement(ns + "ClCompile", include);
                    if (!writeFilter && Path.GetFileName(pair.Key) == "AssemblyInfo.cpp")
                        PrecompiledHeader(ne);
                    else if (writeFilter && !string.IsNullOrEmpty(pair.Value))
                        ne.Add(filter);
                }
                else if (type == ItemType.Resources)
                {
                    ne = new XElement(ns + "EmbeddedResource", include);
                    if (!writeFilter)
                        ne.Add(new XElement(ns + "SubType", "Designer"));
                    else if (!string.IsNullOrEmpty(pair.Value))
                        ne.Add(filter);
                }
                else if (type == ItemType.Other)
                {
                    ne = new XElement(ns + "None", include);
                    if (writeFilter && !string.IsNullOrEmpty(pair.Value))
                        ne.Add(filter);
                }
                else if (type == ItemType.Filters)
                {
                    ne = new XElement(ns + "Filter", include);
                    ne.Add(new XElement(ns + "UniqueIdentifier", pair.Value));
                }

                element.Add(ne);
            }
        }

        void PrecompiledHeader(XElement element)
        {
            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Debug|Win32'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Debug|Win32'"), "stdafx.h"));
            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Debug|x64'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Debug|x64'"), "stdafx.h"));

            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Public|Win32'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Public|Win32'"), "stdafx.h"));
            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Public|x64'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Public|x64'"), "stdafx.h"));

            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Release|Win32'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Release|Win32'"), "stdafx.h"));
            element.Add(new XElement(ns + "PrecompiledHeader", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Release|x64'"), "Create"));
            element.Add(new XElement(ns + "PrecompiledHeaderFile", new XAttribute("Condition", "'$(Configuration)|$(Platform)'=='Release|x64'"), "stdafx.h"));
        }

        XElement FindItemGroup(string action)
        {
            foreach (var element in document.Root.Elements(ns + "ItemGroup"))
            {
                if (element.Element(ns + action) != null)
                    return element;
            }

            return null;
        }
    }
}
