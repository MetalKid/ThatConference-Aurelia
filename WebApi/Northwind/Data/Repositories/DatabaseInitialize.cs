#region << Usings >>

using ThatConference.Data.Northwind;
using ThatConference.Northwind.Interfaces.Repositories;

#endregion

namespace ThatConference.Northwind.Data.Repositories
{
    /// <summary>
    /// Initializes the databases.
    /// </summary>
    public class DatabaseInitialize : IDatabaseInitialize
    {

        /// <summary>
        /// Initializes all databases to the proper state and caches the models.
        /// </summary>
        public void Initalize()
        {
            NorthwindContext.CompileModel();
        }

    }
}
