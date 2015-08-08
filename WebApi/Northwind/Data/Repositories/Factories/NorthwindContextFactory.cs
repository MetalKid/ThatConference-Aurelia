#region << Usings >>

using System.Configuration;
using ThatConference.Data.Common;
using ThatConference.Common;
using ThatConference.Data.Northwind;
using ThatConference.Data.Northwind.Interfaces;
using ThatConference.Northwind.Common;

#endregion

namespace ThatConference.Northwind.Data.Repositories.Factories
{
    /// <summary>
    /// This class is used for getting new connections to the BetterLoanOfficers database.
    /// </summary>
    public class NorthwindContextFactory : INorthwindContextFactory
    {

        /// <summary>
        /// Returns a new connection to the database.
        /// </summary>
        /// <returns>INorthwindContext.</returns>
        public INorthwindContext Create()
        {
            return
                new NorthwindContext(
                    ConfigurationManager.ConnectionStrings[AppSettings.NorthwindConnStringName]
                        .ConnectionString);
        }

    }
}
