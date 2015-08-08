#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[sysdiagrams] table.
    /// </summary>
    internal class sysdiagramsMapping: EntityMapping<sysdiagrams>
    {

        public sysdiagramsMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}