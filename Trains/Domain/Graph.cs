// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graph.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Domain
{
    using System;
    using System.Collections.Generic;

    using Trains.Exceptions;

    /// <summary>
    /// The graph class.
    /// </summary>
    public class Graph : IGraph
    {
        /// <summary>
        /// The comma char representation.
        /// </summary>
        private const char CommaChar = ',';

        /// <summary>
        /// The comma char representation.
        /// </summary>
        private const string SpaceChar = " ";

        /// <summary>
        /// The routes in the circuit.
        /// </summary>
        private readonly LinkedList<IRoute> routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        public Graph()
        {
            this.routes = new LinkedList<IRoute>();
        }

        /// <summary>
        /// Load configurations from a given string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public void LoadConfigs(string input)
        {
            input = input.Replace(SpaceChar, string.Empty);
            var splitedInput = input.Split(CommaChar);

            foreach (var route in splitedInput)
            {
                var origin = route.Substring(0, 1);
                var destination = route.Substring(1, 1);
                var distance = int.Parse(route.Substring(2, 1));

                var r = new Route(origin, destination, distance);
                this.routes.AddLast(r);
            }
        }

        /// <summary>
        /// Calculates the distance for a given route.
        /// </summary>
        /// <param name="route">
        /// The route.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CalculateDistance(string route)
        {
            var towns = route.Split(CommaChar);
            int totalDistance = 0;

            for (int i = 0; i < towns.Length - 1; i++)
            {
                var distance = this.GetDistance(towns[i], towns[i + 1]);

                if (distance == 0)
                {
                    throw new NoSuchRouteException();
                }

                totalDistance += distance;
            }

            return totalDistance;
        }

        /// <summary>
        /// Calculates if a given destination can be reached for more than one route.
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="minStops">
        /// The min stops.
        /// </param>
        /// <param name="maxStops">
        /// The max stops.
        /// </param>
        /// <param name="iteration">
        /// The iteration.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CalculateRoutesToADestination(string origin, string destination, int minStops, int maxStops, int iteration = 0)
        {
            var enumerator = this.routes.GetEnumerator();
            var num = 0;

            while (enumerator.MoveNext())
            {
                var route = enumerator.Current;
                if (route.CompareOrigin(origin) && iteration + 1 < maxStops)
                {
                    num += this.CalculateRoutesToADestination(route.GetDestination(), destination, minStops, maxStops, iteration + 1);
                }

                if (route.CompareOrigin(origin) && route.CompareDestination(destination))
                {
                    if (iteration + 1 >= minStops && iteration + 1 <= maxStops)
                    {
                        num++;
                    }
                }
            }

            return num;
        }

        /// <summary>
        /// Calculates if a given destination can be reached by distance.
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="maxDistance">
        /// The max distance.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CalculateDestinationByDistance(string origin, string destination, int maxDistance, int distance = 0)
        {
            var enumerator = this.routes.GetEnumerator();
            int num = 0;

            while (enumerator.MoveNext())
            {
                var route = enumerator.Current;
                if (route.CompareOrigin(origin) && (distance + route.GetDistance()) < maxDistance)
                {
                    num += this.CalculateDestinationByDistance(
                        route.GetDestination(), destination, maxDistance, distance + route.GetDistance());
                }

                if (route.CompareOrigin(origin) && route.CompareDestination(destination))
                {
                    if (distance + route.GetDistance() < maxDistance)
                    {
                        num++;
                    }
                }
            }

            return num;
        }

        /// <summary>
        /// The calculate shortest route.
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="NoSuchRouteException">
        /// It will be thrown when the route does not exist.
        /// </exception>
        public int CalculateShortestRoute(string origin, string destination)
        {
            var length = this.CalculateShortestRoute(origin, destination, 0);

            if (length == int.MaxValue)
            {
                throw new NoSuchRouteException();
            }

            return length;
        }

        /// <summary>
        /// Returns a distance between origin and destination
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDistance(string origin, string destination)
        {
            return this.ExecuteOnEachItem(
                (route) =>
                    {
                        if (route.CompareOrigin(origin) && route.CompareDestination(destination))
                        {
                            return route.GetDistance();
                        }

                        return 0;
                    });
        }

        /// <summary>
        /// Calculates the total length of the circuit.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Length()
        {
            return this.ExecuteOnEachItem(
                (route) => this.GetDistance(route.GetOrigin(), route.GetDestination()));
        }

        /// <summary>
        /// Checks if the circuit is empty.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.routes.Count == 0;
        }

        /// <summary>
        /// The execute on each item.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int ExecuteOnEachItem(Func<IRoute, int> action)
        {
            int value = 0;

            var enumerator = this.routes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var route = enumerator.Current;
                value += action(route);
            }

            return value;
        }

        /// <summary>
        /// The calculate shortest route between origin and destination
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int CalculateShortestRoute(string origin, string destination, int distance)
        {
            var enumerator = this.routes.GetEnumerator();

            int d2, minLength;

            int d1 = d2 = minLength = int.MaxValue;

            while (enumerator.MoveNext())
            {
                var route = enumerator.Current;
                if (route.CompareOrigin(origin) && distance < this.Length())
                {
                    d1 = this.CalculateShortestRoute(route.GetDestination(), destination, distance + route.GetDistance());
                }

                if (route.CompareOrigin(origin) && route.CompareDestination(destination))
                {
                    d2 = distance + route.GetDistance();
                }

                minLength = Math.Min(Math.Min(d1, d2), minLength);
            }

            return minLength;
        }

    }
}
