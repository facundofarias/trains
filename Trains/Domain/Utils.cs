// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Domain
{
    using System.IO;

    /// <summary>
    /// The utils class. 
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Utils method to load a file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string LoadFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
