#region << Usings >>

using ThatConference.Data.Common;
using ThatConference.Data.Northwind.Interfaces;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Interfaces.Repositories.Northwind;

#endregion

namespace ThatConference.Northwind.Data.Repositories.Northwind
{
    /// <summary>
    /// This class manages all interactions with the [NORTHWND] database.
    /// </summary>
    public class NorthwindRepositoryManager : 
        RepositoryManagerBase<INorthwindContext, INorthwindScope>,
        INorthwindRepositoryManager
    {

        #region << IoC Injected Properties >>
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Categories] table.
        /// </summary>
        public ICategoriesRepository CategoriesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        public ICustomerCustomerDemoRepository CustomerCustomerDemoRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerDemographics] table.
        /// </summary>
        public ICustomerDemographicsRepository CustomerDemographicsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Customers] table.
        /// </summary>
        public ICustomersRepository CustomersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Employees] table.
        /// </summary>
        public IEmployeesRepository EmployeesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[EmployeeTerritories] table.
        /// </summary>
        public IEmployeeTerritoriesRepository EmployeeTerritoriesRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Order Details] table.
        /// </summary>
        public IOrderDetailsRepository OrderDetailsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Orders] table.
        /// </summary>
        public IOrdersRepository OrdersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Products] table.
        /// </summary>
        public IProductsRepository ProductsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Region] table.
        /// </summary>
        public IRegionRepository RegionRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Shippers] table.
        /// </summary>
        public IShippersRepository ShippersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Suppliers] table.
        /// </summary>
        public ISuppliersRepository SuppliersRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[sysdiagrams] table.
        /// </summary>
        public IsysdiagramsRepository sysdiagramsRepository { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Territories] table.
        /// </summary>
        public ITerritoriesRepository TerritoriesRepository { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes in information about the user and database.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="ctx">The current connection to the database.</param>
        public NorthwindRepositoryManager(INorthwindScope scope, INorthwindContextFactory ctx)
            : base(scope, ctx)
        {
        }

        #endregion

    }
}
