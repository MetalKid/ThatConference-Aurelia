#region << Usings >>

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ThatConference.Data.Common;
using ThatConference.Data.Northwind.Entities;
using ThatConference.Data.Northwind.Interfaces;

#endregion

namespace ThatConference.Data.Northwind
{
    /// <summary>
    /// This class is used to access the [NORTHWND] database.
    /// </summary>
    public class NorthwindContext : GenericDbContext, INorthwindContext
    {
    
        #region << Static Methods >>

        /// <summary>
        /// This method will compile the EF model when called.
        /// </summary>
        public static void CompileModel()
        {
            GetCompiledModel(ref _modelCache, ref _lock, BuildModelMapping);
        }

        #endregion

        #region << Variables >>

        private static object _lock = new object();
        private static DbCompiledModel _modelCache;

        #endregion

        #region << DbSets >>
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Categories] table.
        /// </summary>
        public virtual IDbSet<Categories> CategorieCategorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[CustomerCustomerDemo] table.
        /// </summary>
        public virtual IDbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[CustomerDemographics] table.
        /// </summary>
        public virtual IDbSet<CustomerDemographics> CustomerDemographicCustomerDemographicses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Customers] table.
        /// </summary>
        public virtual IDbSet<Customers> CustomerCustomerses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Employees] table.
        /// </summary>
        public virtual IDbSet<Employees> EmployeeEmployeeses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[EmployeeTerritories] table.
        /// </summary>
        public virtual IDbSet<EmployeeTerritories> EmployeeTerritorieEmployeeTerritorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Order Details] table.
        /// </summary>
        public virtual IDbSet<OrderDetails> OrderDetailOrderDetailses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Orders] table.
        /// </summary>
        public virtual IDbSet<Orders> OrderOrderses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Products] table.
        /// </summary>
        public virtual IDbSet<Products> ProductProductses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Region] table.
        /// </summary>
        public virtual IDbSet<Region> Regions { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Shippers] table.
        /// </summary>
        public virtual IDbSet<Shippers> ShipperShipperses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Suppliers] table.
        /// </summary>
        public virtual IDbSet<Suppliers> SupplierSupplierses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[sysdiagrams] table.
        /// </summary>
        public virtual IDbSet<sysdiagrams> sysdiagramsysdiagramses { get; set; }
        
        /// <summary>
        /// Gets or sets the reference to the [dbo].[Territories] table.
        /// </summary>
        public virtual IDbSet<Territories> TerritorieTerritorieses { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that uses the connection string based uppon the country code.
        /// </summary>
        /// <param name="connStringName">The configuration data of the current country to connect to.</param>
        public NorthwindContext(string connStringName)
            : base(connStringName, GetCompiledModel(ref _modelCache, ref _lock, BuildModelMapping))
        {
            Configuration.LazyLoadingEnabled = false;
            // We set the entity state directly so no need to turn this on
            Configuration.AutoDetectChangesEnabled = false;
            // This must be false to utilize our Generic Repository approach
            Configuration.ProxyCreationEnabled = false; 
        }

        /// <summary>
        /// Turns off the configuration to check for EdmMetaData and __MigrationHistory.
        /// </summary>
        static NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }

        #endregion

        #region << Overridden Methods >>

        /// <summary>
        /// Creates the entity tree.
        /// </summary>
        /// <param name="modelBuilder">The builder to add the configurations to.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            BuildModelMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region << Model Mapping >>

        /// <summary>
        /// Adds all of the Fluent API code to map the various entities.
        /// </summary>
        /// <param name="modelBuilder">The builder to add the configurations to.</param>
        private static void BuildModelMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof (NorthwindContext).Assembly);
        }

        #endregion
        
    }
}