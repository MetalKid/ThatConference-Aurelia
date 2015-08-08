#region << Usings >>

using ThatConference.Northwind.Interfaces.Services;

#endregion

namespace ThatConference.Northwind.Services.Interfaces
{
    /// <summary>
    /// This interface defines methods to working directly with databases to be called by other services.
    /// </summary>
    public interface IDatabaseServiceInternal : IDatabaseService
    {
    }
}
