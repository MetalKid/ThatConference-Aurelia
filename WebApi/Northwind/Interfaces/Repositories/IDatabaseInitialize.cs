#region << Usings >>

using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Interfaces.Repositories
{
    /// <summary>
    /// Initializes the databases.
    /// </summary>
    public interface IDatabaseInitialize : IIoC
    {

        /// <summary>
        /// Initializes all databases to the proper state and caches the models.
        /// </summary>
        void Initalize();

    }
}