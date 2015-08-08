#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[CustomerCustomerDemo] table.
    /// </summary>
    internal class CustomerCustomerDemoMapping: EntityMapping<CustomerCustomerDemo>
    {

        public CustomerCustomerDemoMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}