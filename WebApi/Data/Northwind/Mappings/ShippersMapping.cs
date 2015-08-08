#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Shippers] table.
    /// </summary>
    internal class ShippersMapping: EntityMapping<Shippers>
    {

        public ShippersMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}