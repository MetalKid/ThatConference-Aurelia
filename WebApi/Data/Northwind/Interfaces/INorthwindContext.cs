#region << Usings >>

using System.Data.Entity;
using ThatConference.Data.Common.Interfaces;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Interfaces
{
    /// <summary>
    /// This interface is used to define the tables to access from the [NORTHWND] database.
    /// </summary>
    public interface INorthwindContext : IDbContext
    {
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Categories] table.
        /// </summary>
        IDbSet<Categories> CategorieCategorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        IDbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[CustomerDemographics] table.
        /// </summary>
        IDbSet<CustomerDemographics> CustomerDemographicCustomerDemographicses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Customers] table.
        /// </summary>
        IDbSet<Customers> CustomerCustomerses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Employees] table.
        /// </summary>
        IDbSet<Employees> EmployeeEmployeeses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[EmployeeTerritories] table.
        /// </summary>
        IDbSet<EmployeeTerritories> EmployeeTerritorieEmployeeTerritorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Order Details] table.
        /// </summary>
        IDbSet<OrderDetails> OrderDetailOrderDetailses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Orders] table.
        /// </summary>
        IDbSet<Orders> OrderOrderses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Products] table.
        /// </summary>
        IDbSet<Products> ProductProductses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Region] table.
        /// </summary>
        IDbSet<Region> Regions { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Shippers] table.
        /// </summary>
        IDbSet<Shippers> ShipperShipperses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Suppliers] table.
        /// </summary>
        IDbSet<Suppliers> SupplierSupplierses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[sysdiagrams] table.
        /// </summary>
        IDbSet<sysdiagrams> sysdiagramsysdiagramses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Territories] table.
        /// </summary>
        IDbSet<Territories> TerritorieTerritorieses { get; set; }

    }
}