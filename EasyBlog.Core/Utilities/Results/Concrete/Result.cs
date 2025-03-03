using EasyBlog.Core.Entities.Concrete;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;

namespace EasyBlog.Core.Utilities.Results.Concrete;

public class Result : IResult
{
    public Result(
        ResultStatus resultStatus,
        string message = null,
        Exception exception = null,
        IEnumerable<ValidationError> validationErrors = null)
    {
        ResultStatus = resultStatus;
        Message = message;
        Exception = exception;
        ValidationErrors = validationErrors;
    }

    public ResultStatus ResultStatus { get; }
    public string Message { get; }
    public Exception Exception { get; }
    public IEnumerable<ValidationError> ValidationErrors { get; }
}