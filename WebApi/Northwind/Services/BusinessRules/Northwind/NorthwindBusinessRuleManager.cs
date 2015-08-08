#region << Usings >>

using ThatConference.Northwind.Services.Interfaces.Northwind.BusinessRules;
using ThatConference.Services.Common;

#endregion

namespace ThatConference.Northwind.Services.BusinessRules.Northwind
{
    /// <summary>
    /// This class defines all business rules that could be needed when working with 
    /// Northwind entities.
    /// </summary>
    public class NorthwindBusinessRuleManager : 
        BusinessRuleManagerBase, 
        INorthwindBusinessRuleManager
    {

        #region << IoC Injected Properties >>
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Categories] table.
        /// </summary>
        public ICategoriesBusinessRules CategoriesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        public ICustomerCustomerDemoBusinessRules CustomerCustomerDemoBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[CustomerDemographics] table.
        /// </summary>
        public ICustomerDemographicsBusinessRules CustomerDemographicsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Customers] table.
        /// </summary>
        public ICustomersBusinessRules CustomersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Employees] table.
        /// </summary>
        public IEmployeesBusinessRules EmployeesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[EmployeeTerritories] table.
        /// </summary>
        public IEmployeeTerritoriesBusinessRules EmployeeTerritoriesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Order Details] table.
        /// </summary>
        public IOrderDetailsBusinessRules OrderDetailsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Orders] table.
        /// </summary>
        public IOrdersBusinessRules OrdersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Products] table.
        /// </summary>
        public IProductsBusinessRules ProductsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Region] table.
        /// </summary>
        public IRegionBusinessRules RegionBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Shippers] table.
        /// </summary>
        public IShippersBusinessRules ShippersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Suppliers] table.
        /// </summary>
        public ISuppliersBusinessRules SuppliersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[sysdiagrams] table.
        /// </summary>
        public IsysdiagramsBusinessRules sysdiagramsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the default business rules that validate the [dbo].[Territories] table.
        /// </summary>
        public ITerritoriesBusinessRules TerritoriesBusinessRules { get; set; }

        #endregion

    }
}
