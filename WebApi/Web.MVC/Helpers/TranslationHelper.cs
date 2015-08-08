#region << Usings >>

using System;

#endregion

namespace MetalKid.Games.UI.Web.Mvc.Helpers
{
    internal static class TranslationHelper
    {
        public static string GenericError(Guid? elmahId)
        {
            return elmahId.HasValue
                ? string.Format("An unexpected error has occurred.  Please try again. Reference number: {0}.", elmahId)
                : "An unexpected error has occurred.  Please try again.";
        }
    }
}
