#region << Usings >>

using ThatConference.Common.Interfaces;
using ThatConference.Northwind.Interfaces.Repositories;
using ThatConference.Northwind.Services.Interfaces;

#endregion

namespace ThatConference.Northwind.Services
{
    /// <summary>
    /// This interface defines methods related to working directly with databases.
    /// </summary>
    public class DatabaseService : IDatabaseServiceInternal, IIoC
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the reference to the instance of IDatabaseInitialize.
        /// </summary>
        public IDatabaseInitialize DatabaseInitialize { get; private set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseInitialize">Reference to the instance of IDatabaseInitialize.</param>
        public DatabaseService(IDatabaseInitialize databaseInitialize)
        {
            DatabaseInitialize = databaseInitialize;
        }

        #endregion

        #region << IDatabaseService Methods >>

        /// <summary>
        /// Initializes any databse related items at startup.  (i.e. Compiles EF model)
        /// </summary>
        public void Initialize()
        {
            DatabaseInitialize.Initalize();
        }

        #endregion

    }
}
