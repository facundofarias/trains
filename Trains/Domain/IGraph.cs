// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGraph.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Domain
{
    /// <summary>
    /// The Graph interface.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Load configurations from a given string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        void LoadConfigs(string input);

        /// <summary>
        /// Calculates the distance for a given route.
        /// </summary>
        /// <param name="route">
        /// The route.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CalculateDistance(string route);

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
        int CalculateRoutesToADestination(string origin, string destination, int minStops, int maxStops, int iteration = 0);

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
        int CalculateDestinationByDistance(string origin, string destination, int maxDistance, int distance = 0);

        /// <summary>
        /// The calculate shortest route between origin and destination
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
        int CalculateShortestRoute(string origin, string destination);

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
        int GetDistance(string origin, string destination);

        /// <summary>
        /// Calculates the total length of the circuit.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Length();

        /// <summary>
        /// Checks if the circuit is empty.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsEmpty();

    }
}
