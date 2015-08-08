#region << Usings >>

using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Interfaces.Repositories.Northwind
{
    /// <summary>
    /// This interface defines all repositories used in the [NORTHWND] database.
    /// </summary>
    public interface INorthwindRepositoryManager : IRepositoryManager
    {

        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Categories] table.
        /// </summary>
        ICategoriesRepository CategoriesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        ICustomerCustomerDemoRepository CustomerCustomerDemoRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerDemographics] table.
        /// </summary>
        ICustomerDemographicsRepository CustomerDemographicsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Customers] table.
        /// </summary>
        ICustomersRepository CustomersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Employees] table.
        /// </summary>
        IEmployeesRepository EmployeesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[EmployeeTerritories] table.
        /// </summary>
        IEmployeeTerritoriesRepository EmployeeTerritoriesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Order Details] table.
        /// </summary>
        IOrderDetailsRepository OrderDetailsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Orders] table.
        /// </summary>
        IOrdersRepository OrdersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Products] table.
        /// </summary>
        IProductsRepository ProductsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Region] table.
        /// </summary>
        IRegionRepository RegionRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Shippers] table.
        /// </summary>
        IShippersRepository ShippersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Suppliers] table.
        /// </summary>
        ISuppliersRepository SuppliersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[sysdiagrams] table.
        /// </summary>
        IsysdiagramsRepository sysdiagramsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Territories] table.
        /// </summary>
        ITerritoriesRepository TerritoriesRepository { get; set; }

    }
}
