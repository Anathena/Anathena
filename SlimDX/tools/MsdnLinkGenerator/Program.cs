using System;
using System.Text;
using System.Net;
using System.Xml;

namespace MsdnLinkGenerator
{
	class Program
	{
		const string MsdnLinkPrefix = "http://msdn.";

		static WebClient m_client = new WebClient();
		static string[] m_searchEngines;
		static int m_currentEngine = 0;
		static int m_searchCount = 0;

		static string GetMsdnLink(string searchString)
		{
			if(string.IsNullOrEmpty(searchString))
				return null;

			try
			{
				string uri = string.Format(m_searchEngines[m_currentEngine], searchString);
				byte[] data = m_client.DownloadData(uri);
				
				//don't issue more than 50 searches in a row to the same engine
				++m_searchCount;
				if(m_searchCount >= 50)
				{
					m_currentEngine = (m_currentEngine + 1) % m_searchEngines.Length;
					m_searchCount = 0;
				}

				string pageText = Encoding.ASCII.GetString(data);
				int linkPos = pageText.IndexOf(MsdnLinkPrefix);
				if(linkPos < 0)
					return null;
				int endPos = pageText.IndexOf("\"", linkPos);
				if(endPos < 0)
					return null;

				string link = pageText.Substring(linkPos, endPos - linkPos);
				return link;
			}
			catch(System.Net.WebException)
			{
				Console.WriteLine("Failed to search, moving to a new engine...");
				++m_currentEngine;
				if(m_currentEngine >= m_searchEngines.Length)
				{
					m_currentEngine = 0;
					//take some time to let the various engines cool off
					Console.WriteLine("Pausing queries for twenty minutes...");
					System.Threading.Thread.Sleep(20 * 60 * 1000);
				}

				return GetMsdnLink(searchString);
			}
		}

		static string ExtractLinkCore(string uri)
		{
			if(string.IsNullOrEmpty(uri))
				return null;

			try
			{

				int startPos = uri.LastIndexOf('/');
				int endPos = uri.LastIndexOf('(');
				//Sometimes the links don't have (VS.85), but it's ok to add them in
				if(endPos < 0)
					endPos = uri.LastIndexOf('.');
				if(startPos < 0 || endPos < 0)
					return null;

				++startPos;
				return uri.Substring(startPos, endPos - startPos);
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		static void ProcessXml(string file)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(file);

			int i = 0;
			XmlNodeList nodes = doc.SelectNodes("//unmanaged");
			foreach(XmlElement element in nodes)
			{
				++i;
				if(element.HasAttribute("href"))
				{
					Console.WriteLine("({2}/{3}) Item {0} is already linked to document: {1}.",
						element.InnerText, element.Attributes["href"].InnerText, i, nodes.Count);
					continue;
				}

				string text = element.InnerText;
				if(text == "None")
				{
					Console.Write("({0}/{1}) Not linked.", i, nodes.Count);
					continue;
				}

				string link = GetMsdnLink(text);
				string core = ExtractLinkCore(link);

				if(string.IsNullOrEmpty(core))
				{
					Console.WriteLine("({0}/{1}) Unable to link {2}.", i, nodes.Count, element.InnerText);
					continue;
				}

				XmlAttribute attrib = doc.CreateAttribute("href");
				attrib.InnerText = core;
				element.Attributes.Append(attrib);
				if(i % 10 == 0)
					doc.Save(file);
				Console.WriteLine("({2}/{3}) Linked \"{0}\" to document: {1}.", text, core, i, nodes.Count);

				System.Threading.Thread.Sleep(2 * 60 * 1000);
			}

			doc.Save(file);
		}

		static void Main(string[] args)
		{
			m_searchEngines = new string[]
			{
				"http://www.google.com/search?hl=en&q={0}+site%3Amsdn.microsoft.com%2Fen-us",
				//"http://search.live.com/results.aspx?q={0}+site%3Amsdn.microsoft.com%2Fen-us&form=QBLH&qs=n",
				"http://search.yahoo.com/search?p={0}+site%3Amsdn.microsoft.com%2Fen-us&fr=yfp-t-120&toggle=1&cop=mss&ei=UTF-8"
			};

			ProcessXml(args[0]);
		}
	}
}
