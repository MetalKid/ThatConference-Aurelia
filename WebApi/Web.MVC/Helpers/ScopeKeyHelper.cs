#region << Usings >>

using System;
using System.Web;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Interfaces.Services;

#endregion

namespace ThatConference.Northwind.Web.Mvc.Helpers
{
    internal static class ScopeKeyHelper
    {

        #region << Constants >>

        private const string SESSION_SCOPE_KEY = "ScopeKey";

        #endregion

        #region << Public Methods >>

        public static INorthwindScopeKey GetScopeKey()
        {
            return HttpContext.Current == null || HttpContext.Current.Session == null
                ? null
                : HttpContext.Current.Session[SESSION_SCOPE_KEY] as INorthwindScopeKey;
        }

        public static void CreateScopeKey()
        {
            if (HttpContext.Current == null || 
                HttpContext.Current.Session == null ||
                HttpContext.Current.Session[SESSION_SCOPE_KEY] != null)
            {
                return;
            }

            var scopeService = IoCConfig.Container.GetInstance<IScopeService>();
            HttpContext.Current.Session[SESSION_SCOPE_KEY] = scopeService.CreateScope(Environment.UserName).Data;
        }

        #endregion

    }
}
