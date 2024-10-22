using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons
{
    public class Polygon : IPolygon

    {
        private double[,] _vertices;
        public double[,] Vertices
        {           
             get => _vertices;
             set
             {
                 if (value.GetLength(0) < 3)
                     throw new ArgumentOutOfRangeException("Each polygon has at least 3 vertices.");
                 if (value.GetLength(1) != 2)
                     throw new ArgumentOutOfRangeException("Each vertex must have exactly two coordinates (x, y).");
                 if (_vertices == null)
                     throw new ArgumentNullException(nameof(_vertices), "Vertices array cannot be null.");
                for (int i = 0; i < value.GetLength(0); i++)
                {
                    for (int j = i + 1; j < value.GetLength(0); j++)
                    {
                        if (value[i, 0] == value[j, 0] && value[i, 1] == value[j, 1])
                            throw new ArgumentOutOfRangeException("Vertices cannot have the same coordinates.");
                    }
                }
                _vertices = value;

             }
        }

        private int _numOfVertices;
        public int NumOfVertices {

             get => _numOfVertices;
             set => _numOfVertices = value < 3 ? throw new ArgumentOutOfRangeException("Number of vertices must be > 30") : value;
        }
        public Polygon()
        {
            _numOfVertices = 3;
            _vertices = new double[_numOfVertices, 2];

        }
        public Polygon(int n)
        {
            _numOfVertices = n;
        }
        public Polygon(int numOfVertices, double[,] vertices)
        {
            if (numOfVertices < 3)
                throw new ArgumentOutOfRangeException(nameof(numOfVertices), "Polygon must have at least 3 vertices.");

            if (vertices.GetLength(0) != numOfVertices)
                throw new ArgumentException("The number of vertices in the array must match the numOfVertices parameter.");

            if (vertices.GetLength(1) != 2)
                throw new ArgumentException("Each vertex must have exactly two coordinates (x, y).");

            _numOfVertices = numOfVertices;
            _vertices = new double[_numOfVertices, 2];

            for (int i = 0; i < _numOfVertices; i++)
            {
                _vertices[i, 0] = vertices[i, 0];
                _vertices[i, 1] = vertices[i, 1];
            }
        }
        
        public Polygon(Polygon p) : this(p.NumOfVertices, p.Vertices)
        {
        }
        public void AddVertex(double x, double y)
        {
            if (NumOfVertices >= _vertices.GetLength(0))
            {
                double[,] v = new double[NumOfVertices + 1, 2];
                for (int i = 0; i < NumOfVertices; i++)
                {
                    v[i, 0] = _vertices[i, 0];
                    v[i, 1] = _vertices[i, 1];
                }
                _vertices = v;
            }

            _vertices[NumOfVertices, 0] = x;
            _vertices[NumOfVertices, 1] = y;
            NumOfVertices++;
        }
        public double CalculateArea()
        {
            double area = 0;

            for (int i = 0; i < NumOfVertices; i++)
            {
                int next = (i + 1) % NumOfVertices; 
                area += _vertices[i, 0] * _vertices[next, 1] - _vertices[next, 0] * _vertices[i, 1];
            }

            return Math.Abs(area) / 2.0;
        }

        public string DeterminePolygonType()
        {
            switch (NumOfVertices)
            {
                case 3:
                    return "Triangle";
                case 4:
                    return "Square";
    
                default:
                    return $"{NumOfVertices}-gon";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Polygon other = (Polygon)obj;

            if (NumOfVertices != other.NumOfVertices)
                return false;

            for (int i = 0; i < NumOfVertices; i++)
            {
                if (_vertices[i, 0] != other._vertices[i, 0] || _vertices[i, 1] != other._vertices[i, 1])
                    return false;
            }

            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Polygon with {NumOfVertices} vertices:");

            for (int i = 0; i < NumOfVertices; i++)
            {
                sb.AppendLine($"Vertex {i + 1}: ({_vertices[i, 0]}, {_vertices[i, 1]})");
            }

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hashCode = 651103304;
            hashCode = hashCode * -1521134295 + EqualityComparer<double[,]>.Default.GetHashCode(_vertices);
            hashCode = hashCode * -1521134295 + EqualityComparer<double[,]>.Default.GetHashCode(Vertices);
            hashCode = hashCode * -1521134295 + _numOfVertices.GetHashCode();
            hashCode = hashCode * -1521134295 + NumOfVertices.GetHashCode();
            return hashCode;
        }
    }
}
