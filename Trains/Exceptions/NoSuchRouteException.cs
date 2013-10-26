// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoSuchRouteException.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Exceptions
{
    using System;
    
    /// <summary>
    /// The no such route exception.
    /// </summary>
    public class NoSuchRouteException : Exception
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        public override string Message
        {
            get { return "NO SUCH ROUTE"; }
        }
    }
}
