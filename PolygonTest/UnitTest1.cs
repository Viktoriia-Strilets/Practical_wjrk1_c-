using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polygons;
using System;

namespace PolygonTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SetNumOfVertices_SetNumOfVerticesWithWrongValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int NUM_OF_VERTICES = 2;
            IPolygon polygon = new Polygon();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => polygon.NumOfVertices = NUM_OF_VERTICES);
        }
        [TestMethod]
        public void SetNumOfVertices_SetNumOfVerticesWithRightValue_NumOfVerticesSet()
        {
            // Arrange
            const int NUM_OF_VERTICES = 4;
            IPolygon polygon = new Polygon();
            int expected = NUM_OF_VERTICES;

            // Act
            polygon.NumOfVertices = NUM_OF_VERTICES;
            int actual = polygon.NumOfVertices;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetVertices_SetValidArray_SetVertices()
        {
            // Arrange
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 } };
            IPolygon polygon = new Polygon();            

            // Act
            polygon.Vertices = vertices;

            // Assert
            Assert.AreEqual(vertices, polygon.Vertices);
        }     
        [TestMethod]
        public void SetVertices_SetSameCoordinates_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double[,] vertices = { { 0, 0 }, { 0, 0 }, { 2, 5 }, { 2, 5} };
            IPolygon polygon = new Polygon();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => polygon.Vertices = vertices);

        }
        [TestMethod]
        public void SetVertices_SetInvalidArray_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double[,] vertices = { { 0, 0 }, { 1, 1 } };
            IPolygon polygon = new Polygon();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => polygon.Vertices = vertices);

        }
        [TestMethod]
        public void SetVertices_EmptyArray_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double[,] vertices = new double[0, 0];
            IPolygon polygon = new Polygon();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Polygon(0, vertices));

        }
        [TestMethod]
        public void AddVertex_AddValidVertex_StoresCorrectCoordinates()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double x = 3.5;
            double y = 4.2;

            // Act
            polygon.AddVertex(x, y);
            
            // Assert
            Assert.AreEqual(3.5, polygon.Vertices[polygon.NumOfVertices - 1, 0]); 
            Assert.AreEqual(4.2, polygon.Vertices[polygon.NumOfVertices - 1, 1]);
        }
        [TestMethod]
        public void CalculateArea_PolygonWithPositiveCoordinates_ReturnCorrectArea()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 1, 1 }, { 4, 1 }, { 1, 5 } };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 3;

            // Act
            double area = polygon.CalculateArea(); 

            // Assert
            Assert.AreEqual(6, area, 0.0001); 
        }
        [TestMethod]
        public void CalculateArea_PolygonWithNegativeCoordinates_ReturnCorrectArea()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { -1, -1 }, { -5, -1 }, { -5, -5 }, { -1, -5 } };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 4;

            // Act
            double area = polygon.CalculateArea();

            // Assert
            Assert.AreEqual(16, area, 0.0001);
        }
        [TestMethod]
        public void CalculateArea_CollinearPoints_ReturnZero()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 6, 0 } };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 3;

            // Act
            double area = polygon.CalculateArea();

            // Assert
            Assert.AreEqual(0, area);
        }
        [TestMethod]
        public void DeterminePolygonType_Triangle_ReturnTriangle()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 } };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 3;

            // Act
            string type = polygon.DeterminePolygonType(); 

            // Assert
            Assert.AreEqual("Triangle", type);
        }
        [TestMethod]
        public void DeterminePolygonType_Square_ReturnSquare()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 }, { 1, 6} };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 4;

            // Act
            string type = polygon.DeterminePolygonType();

            // Assert
            Assert.AreEqual("Square", type);
        }
        [TestMethod]
        public void DeterminePolygonType_NGon_ReturnNGon()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 }, { 0, 5 }, { 1,8} };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 5;

            // Act
            string type = polygon.DeterminePolygonType();

            // Assert
            Assert.AreEqual($"{polygon.NumOfVertices}-gon", type);
        }
        [TestMethod]
        public void Equals_SamePolygon_ReturnTrue()
        {
            // Arrange
            IPolygon polygon1 = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 } };
            polygon1.Vertices = vertices;
            polygon1.NumOfVertices = 3;

            IPolygon polygon2 = new Polygon();
            polygon2.Vertices = vertices;
            polygon2.NumOfVertices = 3;

            // Act
            bool areEqual = polygon1.Equals(polygon2);

            // Assert
            Assert.IsTrue(areEqual);
        }
        [TestMethod]
        public void Equals_DifferentPolygon_ReturnFalse()
        {
            // Arrange
            IPolygon polygon1 = new Polygon();
            polygon1.Vertices = new double[,] { { 0, 0 }, { 3, 0 }, { 0, 4 } };
            polygon1.NumOfVertices = 3;

            var polygon2 = new Polygon();
            polygon2.Vertices = new double[,] { { 0, 0 }, { 4, 0 }, { 0, 5 }, { 6, 3 } };
            polygon2.NumOfVertices = 4;

            // Act
            bool areEqual = polygon1.Equals(polygon2);

            // Assert
            Assert.IsFalse(areEqual);
        }
        [TestMethod]
        public void ToString_Triangle_ReturnsCorrectString()
        {
            // Arrange
            IPolygon polygon = new Polygon();
            double[,] vertices = { { 0, 0 }, { 3, 0 }, { 0, 4 } };
            polygon.Vertices = vertices;
            polygon.NumOfVertices = 3;

            string expected =
                $"Polygon with 3 vertices:{Environment.NewLine}" +
                $"Vertex 1: (0, 0){Environment.NewLine}" +
                $"Vertex 2: (3, 0){Environment.NewLine}" +
                $"Vertex 3: (0, 4){Environment.NewLine}";

            // Act
            string result = polygon.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
