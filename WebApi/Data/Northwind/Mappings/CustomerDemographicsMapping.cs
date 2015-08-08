#region << Usings >>

using ThatConference.Data.Common.EntityFramework;
using ThatConference.Data.Northwind.Entities;

#endregion

namespace ThatConference.Data.Northwind.Mappings
{
    /// <summary>
    /// Fluent API code to define the [dbo].[CustomerDemographics] table.
    /// </summary>
    internal class CustomerDemographicsMapping: EntityMapping<CustomerDemographics>
    {

        public CustomerDemographicsMapping()
        {
            Ignore(a => a.Timestamp);
        }

    }
}