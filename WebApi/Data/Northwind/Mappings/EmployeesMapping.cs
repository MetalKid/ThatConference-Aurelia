#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[Employees] table.
    /// </summary>
    internal class EmployeesMapping: EntityMapping<Employees>
    {

        public EmployeesMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}