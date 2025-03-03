using Microsoft.AspNetCore.Mvc.ModelBinding;
using NToastNotify;

namespace EasyBlog.Web.Helpers;

public static class ToastHelper
{
    public static void AddErrorsToToastNotification(ModelStateDictionary modelState, IToastNotification toastNotification)
    {
        foreach (var error in modelState.Values.SelectMany(v => v.Errors))
            toastNotification.AddErrorToastMessage(error.ErrorMessage);
    }
}