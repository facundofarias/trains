// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Route.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Domain
{
    /// <summary>
    /// The route class.
    /// </summary>
    public class Route : IRoute
    {
        /// <summary>
        /// The route origin.
        /// </summary>
        private readonly string origin;

        /// <summary>
        /// The route destination.
        /// </summary>
        private readonly string destination;

        /// <summary>
        /// The route distance.
        /// </summary>
        private readonly int distance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
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
        public Route(string origin, string destination, int distance)
        {
            this.origin = origin;
            this.destination = destination;
            this.distance = distance;
        }

        /// <summary>
        /// The get origin.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetOrigin()
        {
            return this.origin;
        }

        /// <summary>
        /// The get destination.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDestination()
        {
            return this.destination;
        }

        /// <summary>
        /// The get distance.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDistance()
        {
            return this.distance;
        }

        /// <summary>
        /// A method to compare origins.
        /// </summary>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CompareOrigin(string origin)
        {
            return this.origin.Equals(origin);
        }

        /// <summary>
        /// A method to compare destinations.
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CompareDestination(string destination)
        {
            return this.destination.Equals(destination);
        }
    }
}
