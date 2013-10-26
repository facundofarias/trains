// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoute.cs" company="Trains">
//   Trains Corp
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Trains.Domain
{
    public interface IRoute
    {
        string GetOrigin();

        string GetDestination();

        int GetDistance();

        bool CompareOrigin(string origin);

        bool CompareDestination(string destination);

    }
}
