// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains
{
    using System;
    using System.Globalization;

    using Trains.Domain;
    using Trains.Exceptions;

    /// <summary>
    /// The main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The input file name.
        /// </summary>
        private const string FileName = "./Resources/input.txt";

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var graph = new Graph();
            var input = Utils.LoadFile(FileName);
            graph.LoadConfigs(input);

            Console.WriteLine("Output #1 : " + ExecuteFunction(() => graph.CalculateDistance("A,B,C")));
            Console.WriteLine("Output #2 : " + ExecuteFunction(() => graph.CalculateDistance("A,D")));
            Console.WriteLine("Output #3 : " + ExecuteFunction(() => graph.CalculateDistance("A,D,C")));
            Console.WriteLine("Output #4 : " + ExecuteFunction(() => graph.CalculateDistance("A,E,B,C,D")));
            Console.WriteLine("Output #5 : " + ExecuteFunction(() => graph.CalculateDistance("A,E,D")));
            Console.WriteLine(
                "Output #6 : " + ExecuteFunction(() => graph.CalculateRoutesToADestination("C", "C", 1, 3)));
            Console.WriteLine(
                "Output #7 : " + ExecuteFunction(() => graph.CalculateRoutesToADestination("A", "C", 4, 4)));
            Console.WriteLine("Output #8 : " + ExecuteFunction(() => graph.CalculateShortestRoute("A", "C")));
            Console.WriteLine("Output #8 : " + ExecuteFunction(() => graph.CalculateShortestRoute("B", "B")));
            Console.WriteLine("Output #10 : " + ExecuteFunction(() => graph.CalculateDestinationByDistance("C", "C", 30)));
        }

        /// <summary>
        /// The execute function.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ExecuteFunction(Func<int> action)
        {
            try
            {
                return action().ToString(CultureInfo.InvariantCulture);
            }
            catch (NoSuchRouteException e)
            {
                return e.Message;
            }
        }
    }
}
