#region << Usings >>

using System;
using ThatConference.Common;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Logging;
using ThatConference.Data.Common;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Services.Helpers;
using ThatConference.Services.Common;

#endregion

namespace ThatConference.Northwind.Services.Common
{
    /// <summary>
    /// This class serves as the base service for all "Games" specific services.
    /// </summary>
    public class NorthwindServiceBase : ServiceBase<INorthwindScope, INorthwindScopeKey>
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        /// <param name="instances">The instances to load.</param>
        protected NorthwindServiceBase(INorthwindScopeKey scopeKey, params object[] instances)
            : base(scopeKey, instances)
        {
        }

        #endregion

        #region << Scope Methods >>

        /// <summary>
        /// Returns the scope to use for this call given the key value.
        /// </summary>
        /// <param name="key">The key to get back the Scope object.</param>
        /// <returns>IScope.</returns>
        public override INorthwindScope GetScope(INorthwindScopeKey key)
        {
            return ScopeHelper.GetScope(key);
        }

        #endregion

        #region << Exception Methods >>

        /// <summary>
        /// This method is called when a method surrounded by a Run() call throws an exception.
        /// </summary>
        /// <param name="result">Result of the currently running process.</param>
        /// <param name="ex">The exception occurred.</param>
        /// <param name="methodName">The name of the method that was called.</param>
        /// <param name="parameters">The parameters used to call this method.</param>
        protected override void HandleRunException(
            IResult result, 
            Exception ex, 
            string methodName,
            params object[] parameters)
        {
            //TODO: Handle logging however you want
            Logger.LogError(methodName, ex);
            base.HandleRunException(result, ex, methodName, parameters);
        }

        #endregion

    }

    /// <summary>
    /// This class serves as the base service for all entity specific "Games" services.
    /// </summary>
    public abstract class NorthwindServiceBase<T1, T2, T3> : ServiceBase<T1, T2, T3, INorthwindScope, INorthwindScopeKey>
        where T1 : SaveableDataTransferObjectBase, new()
        where T2 : EntityBase, new()
        where T3 : FilterBase, new()
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        /// <param name="instances">The instances to load.</param>
        protected NorthwindServiceBase(INorthwindScopeKey scopeKey, params object[] instances)
            : base(scopeKey, instances)
        {
        }

        #endregion

        #region << Scope Methods >>

        /// <summary>
        /// Returns the scope to use for this call given the key value.
        /// </summary>
        /// <param name="key">The key to get back the Scope object.</param>
        /// <returns>IScope.</returns>
        public override INorthwindScope GetScope(INorthwindScopeKey key)
        {
            return ScopeHelper.GetScope(key);
        }

        #endregion

        #region << Exception Methods >>

        /// <summary>
        /// This method is called when a method surrounded by a Run() call throws an exception.
        /// </summary>
        /// <param name="result">Result of the currently running process.</param>
        /// <param name="ex">The exception occurred.</param>
        /// <param name="methodName">The name of the method that was called.</param>
        /// <param name="parameters">The parameters used to call this method.</param>
        protected override void HandleRunException(
            IResult result,
            Exception ex, 
            string methodName, 
            params object[] parameters)
        {
            //TODO: Handle logging however you want
            Logger.LogError(methodName, ex);
            base.HandleRunException(result, ex, methodName, parameters);
        }

        #endregion

    }
}