#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Suppliers] table.
    /// </summary>
    internal class SuppliersMapping: EntityMapping<Suppliers>
    {

        public SuppliersMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}