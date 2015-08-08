#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Region] table.
    /// </summary>
    internal class RegionMapping: EntityMapping<Region>
    {

        public RegionMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}