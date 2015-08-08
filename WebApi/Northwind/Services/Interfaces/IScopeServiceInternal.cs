#region << Usings >>

using ThatConference.Northwind.Interfaces.Services;

#endregion

namespace ThatConference.Northwind.Services.Interfaces
{
    /// <summary>
    /// This internal interface defines methods that can be called only by other services.
    /// </summary>
    public interface IScopeServiceInternal : IScopeService
    {
    }
}
