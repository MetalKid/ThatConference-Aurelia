#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[EmployeeTerritories] table.
    /// </summary>
    internal class EmployeeTerritoriesMapping: EntityMapping<EmployeeTerritories>
    {

        public EmployeeTerritoriesMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}