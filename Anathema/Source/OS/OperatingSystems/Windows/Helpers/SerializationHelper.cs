/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Binarysharp.MemoryManagement.Helpers
{
    /// <summary>
    /// Static helper class providing tools for serializing/deserializing objects.
    /// </summary>
    public static class SerializationHelper
    {
        #region ExportToXmlFile
        /// <summary>
        /// Serializes the specified object and writes the XML document to the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="Object">The object to serialize.</param>
        /// <param name="Path">The path where the file is saved.</param>
        /// <param name="Encoding">The encoding to generate.</param>
        public static void ExportToXmlFile<T>(T Object, String Path, Encoding Encoding)
        {
            // Create the stream to write into the specified file
            using (StreamWriter StreamWriter = new StreamWriter(Path, false, Encoding))
            {
                // Write the content by calling the method to serialize the object
                StreamWriter.Write(ExportToXmlString(Object));
            }
        }

        /// <summary>
        /// Serializes the specified object and writes the XML document to the specified path using <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="Object">The object to serialize.</param>
        /// <param name="Path">The path where the file is saved.</param>
        public static void ExportToXmlFile<T>(T Object, String Path)
        {
            ExportToXmlFile(Object, Path, Encoding.UTF8);
        }

        #endregion

        #region ExportToXmlString
        /// <summary>
        /// Serializes the specified object and returns the XML document.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="Object">The object to serialize.</param>
        /// <returns>XML document of the serialized object.</returns>
        public static string ExportToXmlString<T>(T Object)
        {
            // Initialize the required objects for serialization
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter StringWriter = new StringWriter())
            {
                // Serialize the object
                XmlSerializer.Serialize(StringWriter, Object);

                // Return the serialized object
                return StringWriter.ToString();
            }
        }

        #endregion

        #region ImportFromXmlFile
        /// <summary>
        /// Deserializes the specified file into an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="Path">The path where the object is read.</param>
        /// <param name="Encoding">The character encoding to use. </param>
        /// <returns>The deserialized object.</returns>
        public static T ImportFromXmlFile<T>(String Path, Encoding Encoding)
        {
            // Create the stream to read the specified file
            using (StreamReader StreamReader = new StreamReader(Path, Encoding))
            {
                // Read the content of the file and call the method to deserialize the object
                return ImportFromXmlString<T>(StreamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Deserializes the specified file into an object using <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="Path">The path where the object is read.</param>
        /// <returns>The deserialized object.</returns>
        public static T ImportFromXmlFile<T>(String Path)
        {
            return ImportFromXmlFile<T>(Path, Encoding.UTF8);
        }

        #endregion

        #region ImportFromXmlString
        /// <summary>
        /// Deserializes the XML document to the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="SerializedObject">The string representing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T ImportFromXmlString<T>(String SerializedObject)
        {
            // Initialize the required objects for deserialization
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader StringReader = new StringReader(SerializedObject))
            {
                // Return the serialized object
                return (T)XmlSerializer.Deserialize(StringReader);
            }
        }

        #endregion

    } // End class

} // End namespace