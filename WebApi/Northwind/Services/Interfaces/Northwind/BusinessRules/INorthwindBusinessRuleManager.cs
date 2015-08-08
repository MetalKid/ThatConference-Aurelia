namespace ThatConference.Northwind.Services.Interfaces.Northwind.BusinessRules
{
    /// <summary>
    /// This class defines all business rules that could be needed when working with DigimonWorld2 entities.
    /// </summary>
    public interface INorthwindBusinessRuleManager
    {
    
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Categories] table.
        /// </summary>
        ICategoriesBusinessRules CategoriesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        ICustomerCustomerDemoBusinessRules CustomerCustomerDemoBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[CustomerDemographics] table.
        /// </summary>
        ICustomerDemographicsBusinessRules CustomerDemographicsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Customers] table.
        /// </summary>
        ICustomersBusinessRules CustomersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Employees] table.
        /// </summary>
        IEmployeesBusinessRules EmployeesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[EmployeeTerritories] table.
        /// </summary>
        IEmployeeTerritoriesBusinessRules EmployeeTerritoriesBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Order Details] table.
        /// </summary>
        IOrderDetailsBusinessRules OrderDetailsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Orders] table.
        /// </summary>
        IOrdersBusinessRules OrdersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Products] table.
        /// </summary>
        IProductsBusinessRules ProductsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Region] table.
        /// </summary>
        IRegionBusinessRules RegionBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Shippers] table.
        /// </summary>
        IShippersBusinessRules ShippersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Suppliers] table.
        /// </summary>
        ISuppliersBusinessRules SuppliersBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[sysdiagrams] table.
        /// </summary>
        IsysdiagramsBusinessRules sysdiagramsBusinessRules { get; set; }
        
        /// <summary>
        /// Gets or sets the injected instance for the repository working with the [dbo].[Territories] table.
        /// </summary>
        ITerritoriesBusinessRules TerritoriesBusinessRules { get; set; }

    }
}
