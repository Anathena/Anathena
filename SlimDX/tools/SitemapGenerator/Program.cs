using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

namespace SitemapGenerator
{
	class Program
	{
		const string RootString = @"http://slimdx.mdxinfo.com/latestdocs/Default.aspx?topic=";

		static List<string> m_urlList = new List<string>(256);

		static void AddTopic(string rootString, XmlNode parent)
		{
			foreach(XmlNode topic in parent.ChildNodes)
			{
				if(topic.Attributes["file"].Value == string.Empty)
					continue;

				var name = HttpUtility.UrlEncode((string) topic.Attributes["name"].Value);
				m_urlList.Add(rootString + name);

				AddTopic(rootString + name + "/", topic);
			}
		}

		static void WriteSitemap()
		{
			var stream = File.OpenWrite("sitemap.xml");
			var writer = new StreamWriter(stream);

			writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			writer.WriteLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

			foreach(string url in m_urlList)
			{
				writer.WriteLine(string.Format("<url><loc>{0}</loc></url>", url));
			}

			writer.WriteLine("</urlset>");
			writer.Close();
		}

		static void Main(string[] args)
		{
			if (args.Length < 1)
				return;
			if(!File.Exists(args[0]))
				return;

			var contents = new XmlDocument();
			contents.Load(args[0]);
			AddTopic(RootString, contents.SelectSingleNode("topics"));
			WriteSitemap();
		}
	}
}
