/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System;

namespace SlimDX.SampleFramework {
    public static class Smd {
        public static MeshData FromFile(string file) {
            using (var stream = new FileStream(file, FileMode.Open)) {
                return FromStream(stream);
            }
        }

        public static MeshData FromStream(Stream stream) {
            var result = new MeshData();
            using (TextReader reader = new StreamReader(stream)) {
                var line = reader.ReadLine();
                if (line != "version 1") {
                    throw new InvalidDataException(string.Format("Incorrect or unexpected file version directive ('{0}').", line));
                }

                // These blocks are not yet supported.
                ReadBlock(reader, "nodes");
                ReadBlock(reader, "skeleton");

                var triangles = new Queue<string>(ReadBlock(reader, "triangles"));
                while (triangles.Count > 0) {
                    // Ignore the texture.
                    triangles.Dequeue();

                    for (var vertexIndex = 0; vertexIndex < 3; ++vertexIndex) {
                        var triangle = triangles.Dequeue();
                        var fields = triangle.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        result.Positions.Add(
                            new Vector3(
                                ReadFloat(fields[1]),
                                ReadFloat(fields[2]),
                                ReadFloat(fields[3])
                            )
                        );

                        result.Normals.Add(
                            new Vector3(
                                ReadFloat(fields[4]),
                                ReadFloat(fields[5]),
                                ReadFloat(fields[6])
                            )
                        );

                        result.TextureCoordinates.Add(
                            new Vector2(
                                ReadFloat(fields[7]),
                                ReadFloat(fields[8])
                            )
                        );

                        result.Indices.Add(result.Indices.Count);
                    }

                }
            }

            return result;
        }

        static string[] ReadBlock(TextReader reader, string name) {
            var line = reader.ReadLine();
            if (line != name) {
                throw new InvalidDataException(string.Format("Expected a '{0}' block, but found a '{1}' block instead.", name, line));
            }

            var lines = new List<string>();
            line = reader.ReadLine();
            while (line != "end") {
                lines.Add(line);
                line = reader.ReadLine();
            }

            return lines.ToArray();
        }

        static float ReadFloat(string value) {
            float result;
            if (!float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result)) {
                throw new InvalidDataException(string.Format("Expected a floating point value, but found '{0}' instead.", value));
            }

            return result;
        }
    }
}
