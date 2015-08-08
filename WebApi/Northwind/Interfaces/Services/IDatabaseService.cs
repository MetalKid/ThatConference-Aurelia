namespace ThatConference.Northwind.Interfaces.Services
{
    /// <summary>
    /// This interface defines methods related to working directly with databases.
    /// </summary>
    public interface IDatabaseService
    {

        /// <summary>
        /// Initializes any databse related items at startup.  (i.e. Compiles EF model)
        /// </summary>
        void Initialize();

    }
}
