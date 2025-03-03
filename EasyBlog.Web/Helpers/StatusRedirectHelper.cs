using EasyBlog.Core.Utilities.Results.ComplexTypes;
using IResult = EasyBlog.Core.Utilities.Results.Abstract.IResult;

namespace EasyBlog.Web.Helpers;

public static class StatusRedirectHelper
{
    public static bool ShowNotification(IResult result) =>
        result.ResultStatus == ResultStatus.Success ? true : false;
}
