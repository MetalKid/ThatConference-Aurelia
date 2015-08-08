#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Orders] table.
    /// </summary>
    internal class OrdersMapping: EntityMapping<Orders>
    {

        public OrdersMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}