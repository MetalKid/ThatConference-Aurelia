#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Order Details] table.
    /// </summary>
    internal class OrderDetailsMapping: EntityMapping<OrderDetails>
    {

        public OrderDetailsMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}