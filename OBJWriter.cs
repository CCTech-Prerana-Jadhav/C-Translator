using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    internal class OBJWriter
    {
        private List<Point> uniqueVerticesList = new List<Point>();
        private List<Point> uniqueNormalsList = new List<Point>();
        private Dictionary<Point, int> VerticeMap = new Dictionary<Point, int>();
        private Dictionary<Point, int> NormalMap = new Dictionary<Point, int>();

        public void Write(string filename, Triangulation triangulation)
        {
            using (StreamWriter outfile = new StreamWriter(filename))
            {
                foreach (var triangle in triangulation.Triangles)
                {
                    FindAndAddPoint(triangle.P1, uniqueVerticesList, VerticeMap);
                    FindAndAddPoint(triangle.P2, uniqueVerticesList, VerticeMap);
                    FindAndAddPoint(triangle.P3, uniqueVerticesList, VerticeMap);
                    FindAndAddPoint(triangle.Normal, uniqueNormalsList, NormalMap);
                }

                foreach (var pt in uniqueVerticesList)
                {
                    outfile.WriteLine(FormulateVertex(triangulation, pt));
                }

                foreach (var normal in uniqueNormalsList)
                {
                    outfile.WriteLine(FormulateVertexNormal(triangulation, normal));
                }

                foreach (var tri in triangulation.Triangles)
                {
                    outfile.WriteLine(FormulateFace(tri));
                }
            }
        }

        private void FindAndAddPoint(Point point, List<Point> pointList, Dictionary<Point, int> uniqueValueMap)
        {
            if (!uniqueValueMap.ContainsKey(point))
            {
                pointList.Add(point);
                uniqueValueMap[point] = pointList.Count - 1;
            }
        }

        private string FormulateVertex(Triangulation triangulation, Point point)
        {
            double x = triangulation.UniqueNumbers[point.X];
            double y = triangulation.UniqueNumbers[point.Y];
            double z = triangulation.UniqueNumbers[point.Z];

            return $"v {x.ToString("G6", CultureInfo.InvariantCulture)} " +
                   $"{y.ToString("G6", CultureInfo.InvariantCulture)} " +
                   $"{z.ToString("G6", CultureInfo.InvariantCulture)}";
        }

        private string FormulateVertexNormal(Triangulation triangulation, Point point)
        {
            double x = triangulation.UniqueNumbers[point.X];
            double y = triangulation.UniqueNumbers[point.Y];
            double z = triangulation.UniqueNumbers[point.Z];

            return $"vn {x.ToString("G6", CultureInfo.InvariantCulture)} " +
                   $"{y.ToString("G6", CultureInfo.InvariantCulture)} " +
                   $"{z.ToString("G6", CultureInfo.InvariantCulture)}";
        }

        private string FormulateFace(Triangle triangle)
        {
            string new1 = $"{VerticeMap[triangle.P1] + 1}//{NormalMap[triangle.Normal] + 1} " +
                          $"{VerticeMap[triangle.P2] + 1}//{NormalMap[triangle.Normal] + 1} " +
                          $"{VerticeMap[triangle.P3] + 1}//{NormalMap[triangle.Normal] + 1}";
            return $"f {new1}";
        }
    }
}
