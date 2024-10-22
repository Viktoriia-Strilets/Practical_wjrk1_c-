using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons
{
    public interface IPolygon
    {

        int NumOfVertices { get; set; }
        double[,] Vertices { get; set; }
        void AddVertex(double x, double y);
        string DeterminePolygonType();
        double CalculateArea();

    }
}
