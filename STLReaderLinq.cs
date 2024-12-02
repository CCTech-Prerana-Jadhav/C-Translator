using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;

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

            if (File.Exists(fileName))
            {
                var lines = File.ReadLines(fileName)
                                .Where(line => line.Contains("vertex") || line.Contains("normal"));

                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var keyword = parts[0];
                    var numbers = parts.Skip(1) 
                                       .Select(part => double.TryParse(part, out var value) ? value : (double?)null)
                                       .Where(value => value.HasValue)
                                       .Select(value => value.Value)
                                       .ToArray();

                    for (int i = 0; i < 3; i++)
                    {
                        if (!uniqueValueMap.TryGetValue(numbers[i], out int index))
                        {
                            triangulation.UniqueNumbers.Add(numbers[i]);
                            index = triangulation.UniqueNumbers.Count - 1;
                            uniqueValueMap[numbers[i]] = index;
                        }
                        pointIndices.Add(index);
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

