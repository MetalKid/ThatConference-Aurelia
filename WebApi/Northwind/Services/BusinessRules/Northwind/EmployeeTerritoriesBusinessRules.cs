#region << Usings >>

using System.Collections.Generic;
using ThatConference.Common;
using ThatConference.Common.Validation;
using ThatConference.Data.Northwind.Entities;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Services.Interfaces.Northwind.BusinessRules;

#endregion

namespace ThatConference.Northwind.Services.BusinessRules.Northwind
{
    /// <summary>
    /// This class allows for business specific rules to be applied whenever a(n) 
    /// EmployeeTerritories entity is saved.
    /// </summary>
    public class EmployeeTerritoriesBusinessRules : IEmployeeTerritoriesBusinessRules
    {

        /// <summary>
        /// Ensures that the given entities are valid and have not broken any business related rules.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="entity">The entity to be validated.</param>
        public void ApplyRules(IList<BrokenRule> brokenRules, INorthwindScope scope, EmployeeTerritories entity)
        {

        }

    }
}