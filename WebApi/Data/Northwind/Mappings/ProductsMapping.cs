#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Products] table.
    /// </summary>
    internal class ProductsMapping: EntityMapping<Products>
    {

        public ProductsMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}