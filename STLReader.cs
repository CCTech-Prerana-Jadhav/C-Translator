using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Translator
{
    internal class STLReader
    {
        private const double TOLERANCE = 0.0000001;

        public STLReader()
        {
        }

        ~STLReader()
        {
            
        }

        public bool Compare(double a, double b)
        {
            return Math.Abs(a - b) > TOLERANCE ? a < b : false;
        }

        public void Read(string fileName, Triangulation triangulation)
        {
            var uniqueValueMap = new Dictionary<double, int>();
            var pointIndices = new List<int>();
            double[] xyz = new double[3];

            if (File.Exists(fileName))
            {
                var lines = File.ReadAllLines(fileName);

                foreach (var line in lines)
                {
                    var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] == "vertex" || words[i] == "normal")
                        {
                            xyz[0] = double.Parse(words[++i]);
                            xyz[1] = double.Parse(words[++i]);
                            xyz[2] = double.Parse(words[++i]);

                            for (int j = 0; j < 3; j++)
                            {
                                if (!uniqueValueMap.TryGetValue(xyz[j], out int index))
                                {
                                    triangulation.UniqueNumbers.Add(xyz[j]);
                                    index = triangulation.UniqueNumbers.Count - 1;
                                    uniqueValueMap[xyz[j]] = index;
                                }
                                pointIndices.Add(index);
                            }
                        }

                        if (pointIndices.Count == 12)
                        {
                            Point normal = new Point(pointIndices[0], pointIndices[1], pointIndices[2]);
                            Point p1 = new Point(pointIndices[3], pointIndices[4], pointIndices[5]);
                            Point p2 = new Point(pointIndices[6], pointIndices[7], pointIndices[8]);
                            Point p3 = new Point(pointIndices[9], pointIndices[10], pointIndices[11]);
                            Triangle t = new Triangle(normal, p1, p2, p3);
                            triangulation.Triangles.Add(t);
                            pointIndices.Clear();
                        }
                    }
                }
            }
        }
    }
}
