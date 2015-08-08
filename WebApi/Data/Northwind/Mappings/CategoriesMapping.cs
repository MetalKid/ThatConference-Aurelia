#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Categories] table.
    /// </summary>
    internal class CategoriesMapping: EntityMapping<Categories>
    {

        public CategoriesMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}