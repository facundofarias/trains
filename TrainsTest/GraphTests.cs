// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphTests.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TrainsTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Trains.Domain;
    using Trains.Exceptions;

    /// <summary>
    /// The graph tests.
    /// </summary>
    [TestClass]
    public class GraphTests
    {
        /// <summary>
        /// The test if a graph can be created.
        /// </summary>
        [TestMethod]
        public void TestIfAGraphCanBeCreated()
        {
            // Given

            // When
            IGraph graph = new Graph();

            // Then
            Assert.IsNotNull(graph);
        }

        /// <summary>
        /// The test if a graph can be loaded.
        /// </summary>
        [TestMethod]
        public void TestIfAGraphCanBeLoaded()
        {
            // Given
            IGraph graph = new Graph();

            // When
            graph.LoadConfigs("AB1,BC1,CD1,DE1,EH1");

            // Then
            Assert.IsNotNull(graph);
            Assert.IsFalse(graph.IsEmpty());
            Assert.IsTrue(graph.Length() == 5);
        }

        /// <summary>
        /// The test if a graph can be loaded with unexpected spaces.
        /// </summary>
        [TestMethod]
        public void TestIfAGraphCanBeLoadedwithUnexpectedSpaces()
        {
            // Given
            IGraph graph = new Graph();

            // When
            graph.LoadConfigs("AB1,  BC1, CD1,    DE1, EH1");

            // Then
            Assert.IsNotNull(graph);
            Assert.IsFalse(graph.IsEmpty());
            Assert.IsTrue(graph.Length() == 5);
        }

        /// <summary>
        /// The test if a graph can be loaded with unexpected chars and will only take the first one.
        /// </summary>
        [TestMethod]
        public void TestIfAGraphCanBeLoadedWithUnexpectedCharsAndWillOnlyTakeTheFirstOne()
        {
            // Given
            IGraph graph = new Graph();

            // When
            graph.LoadConfigs("AB1:BC1;CD1:DE1;EH1");

            // Then
            Assert.IsNotNull(graph);
            Assert.IsFalse(graph.IsEmpty());
            Assert.IsTrue(graph.Length() == 1);
        }

        /// <summary>
        /// The test if the distance between two town are calculated correctly.
        /// </summary>
        [TestMethod]
        public void TestIfTheDistanceBetweenTwoTownAreCalculatedCorrectly()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");

            // When
            int distance = graph.CalculateDistance("C,D");

            // Then
            Assert.IsTrue(distance == 3);
        }

        /// <summary>
        /// The test if the distance between to town are calculated correctly.
        /// </summary>
        [TestMethod]
        public void TestIfTheDistanceBetweenMultipleTownAreCalculatedCorrectly()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");

            // When
            int distance = graph.CalculateDistance("A,B,C,D,E,H");

            // Then
            Assert.IsTrue(distance == 15);
            Assert.IsTrue(graph.Length() == 15);
        }

        /// <summary>
        /// The test if an exception is thrown when the route does not exist.
        /// </summary>
        [TestMethod]
        public void TestIfAnExceptionIsThrownWhenTheRouteDoesNotExist()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");
            bool exeptionWasThrown = false;

            // When
            try
            {
                graph.CalculateDistance("A,B,Z");
            }
            catch (NoSuchRouteException)
            {
                exeptionWasThrown = true;
            }

            // Then
            Assert.IsTrue(exeptionWasThrown);
        }

        /// <summary>
        /// The test if a destination can be reached by a single route.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCanBeReachedByASingleRoute()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");

            // When
            int routes = graph.CalculateRoutesToADestination("A", "E", 1, 4);

            // Then
            Assert.IsTrue(routes == 1);
        }

        /// <summary>
        /// The test if a destination can be reached by a multiple route.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCanBeReachedByAMultipleRoute()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,AE5,CE3");

            // When
            int routes = graph.CalculateRoutesToADestination("A", "E", 1, 4);

            // Then
            Assert.IsTrue(routes == 3);
        }

        /// <summary>
        /// The test if a destination it is not part of the circuit.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationItIsNotPartOfTheCircuit()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC1");

            // When
            int routes = graph.CalculateRoutesToADestination("A", "D", 1, 4);

            // Then
            Assert.IsTrue(routes == 0);
        }

        /// <summary>
        /// The test if a destination it is not part of the circuit.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCannotBeReachedByMinimumStops()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC1");

            // When
            int routes = graph.CalculateRoutesToADestination("A", "B", 2, 4);

            // Then
            Assert.IsTrue(routes == 0);
        }

        /// <summary>
        /// The test if a destination can be reached with a given distance.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCanBeReachedWithAGivenDistance()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");

            // When
            int routes = graph.CalculateDestinationByDistance("A", "E", 11);

            // Then
            Assert.IsTrue(routes == 1);
        }

        /// <summary>
        /// The test if a destination cannot be reached with a given distance.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCannotBeReachedWithAGivenDistance()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC2,CD3,DE4,EH5");

            // When
            int routes = graph.CalculateDestinationByDistance("A", "E", 10);

            // Then
            Assert.IsTrue(routes == 0);
        }

        /// <summary>
        /// The test if a destination can be reached by multiple routes a given distance.
        /// </summary>
        [TestMethod]
        public void TestIfADestinationCanBeReachedByMultipleRoutesAGivenDistance()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC1,CD1,AC1");

            // When
            int routes = graph.CalculateDestinationByDistance("A", "C", 4);

            // Then
            Assert.IsTrue(routes == 2);
        }

        /// <summary>
        /// The test the shortest route can be calculated.
        /// </summary>
        [TestMethod]
        public void TestTheShortestRouteCanBeCalculated()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC1,CD1,AD1");

            // When
            int shortestRoute = graph.CalculateShortestRoute("A", "D");

            // Then
            Assert.IsTrue(shortestRoute == 1);
        }

        /// <summary>
        /// The test the shortest route cannot be calculated.
        /// </summary>
        [TestMethod]
        public void TestTheShortestRouteCannotBeCalculated()
        {
            // Given
            IGraph graph = new Graph();
            graph.LoadConfigs("AB1,BC1,CD1,AD1");
            bool exceptionWasThrown = false;

            // When
            try
            {
                graph.CalculateShortestRoute("A", "Z");
            }
            catch (NoSuchRouteException)
            {
                exceptionWasThrown = true;
            }

            // Then
            Assert.IsTrue(exceptionWasThrown);
        }
    }
}
