#region << Usings >>

using ThatConference.Northwind.Interfaces.Services.Northwind;

#endregion

namespace ThatConference.Northwind.Services.Interfaces.Northwind
{
    /// <summary>
    /// This internal interface defines methods that can be called only by other services for 
    /// Suppliers entities.
    /// </summary>
    public interface ISuppliersServiceInternal : ISuppliersService
    {
    }
}