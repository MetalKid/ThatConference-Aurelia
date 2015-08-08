#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Customers] table.
    /// </summary>
    internal class CustomersMapping: EntityMapping<Customers>
    {

        public CustomersMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}