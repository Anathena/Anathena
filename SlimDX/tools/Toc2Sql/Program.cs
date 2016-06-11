using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Toc2Sql
{
	class Program
	{
		static string TocDir = null;
		static int TopicIndex = 0;

		static string EscapeSqlString(string literal)
		{
			if(literal == null)
				return "null";

			literal = literal.Replace("'", @"\'");
			return string.Format("'{0}'", literal);
		}

		static void ProcessNode(XmlNode node, int parent, string parentPath, TextWriter writer)
		{
			var topicIdAttrib = node.Attributes["Id"];
			var topicId = topicIdAttrib == null ? null : topicIdAttrib.InnerText;
			var title = node.Attributes["Title"].InnerText;
			var url = node.Attributes["Url"].InnerText;
			url = url.Substring(5, url.Length - 9);

			//special magic! is the URL a GUID? if so, substitute it with a title!
			Guid guidUrl;
			if(Guid.TryParse(url, out guidUrl))
			{
				string urlTitle = title.Replace(' ', '_');
				string topicCopy = string.Format("/html/{0}.htm", urlTitle);
				File.Copy(TocDir + "/" + node.Attributes["Url"].InnerText, TocDir + topicCopy, true);
				url = urlTitle;
			}

			var line = string.Format(@"INSERT INTO TableOfContents (Id, ParentId, TopicId, ParentPath, Title, Url) VALUES ({0}, {1}, {2}, {3}, {4}, {5});",
				TopicIndex, parent < 0 ? "null" : parent.ToString(), EscapeSqlString(topicId), EscapeSqlString(parentPath), EscapeSqlString(title), EscapeSqlString(url));
			writer.WriteLine(line);

			int parentIndex = TopicIndex++;
			parentPath = string.Format("/{0}", parentIndex) + parentPath;
			foreach(XmlNode child in node.ChildNodes)
			{
				ProcessNode(child, parentIndex, parentPath, writer);
			}
		}

		static void Main(string[] args)
		{
			var tocFile = Path.GetFullPath(args[0]);
			var tocDir = Path.GetDirectoryName(tocFile);
			var sqlFile = tocDir + "/toc.sql";
			TocDir = tocDir;

			//set up the preamble for the database insert
			var sqlWriter = new StreamWriter(sqlFile, false, System.Text.Encoding.UTF8);
			sqlWriter.WriteLine(@"DROP TABLE IF EXISTS TableOfContents;");
			sqlWriter.WriteLine(@"CREATE TABLE TableOfContents (Id INT PRIMARY KEY, ParentId INT, TopicId CHAR(36), ParentPath VARCHAR(128), Title VARCHAR(256), Url VARCHAR(256));");
			sqlWriter.WriteLine(@"BEGIN TRANSACTION;");

			//open up the XML TOC and find the root node
			var tocDoc = new XmlDocument();
			tocDoc.Load(tocFile);
			var tocRoot = tocDoc["HelpTOC"];

			//start building our tree
			foreach(XmlNode child in tocRoot.ChildNodes)
			{
				ProcessNode(child, -1, null, sqlWriter);
			}

			sqlWriter.WriteLine("COMMIT;");
			sqlWriter.Close();
		}
	}
}
