#region << Usings >>

using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Data.Northwind.Interfaces
{
    /// <summary>
    /// This interface is used for getting new connections to the Northwind database.
    /// </summary>
    public interface INorthwindContextFactory : IDbContextFactory<INorthwindContext>
    {

    }
}
