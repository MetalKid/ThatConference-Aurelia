#region << Usings >>

using ThatConference.Data.Northwind.Entities;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Services.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Services.Interfaces.Northwind.BusinessRules
{
    /// <summary>
    /// This interface defines business rules to run when attempting to save sysdiagrams entities.
    /// </summary>
    public interface IsysdiagramsBusinessRules : IBusinessRuleValidation<sysdiagrams, INorthwindScope>
    {
    }
}
