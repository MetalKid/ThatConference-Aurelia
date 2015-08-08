#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Territories] table.
    /// </summary>
    internal class TerritoriesMapping: EntityMapping<Territories>
    {

        public TerritoriesMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}