using EasyBlog.Core.Entities.Concrete;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;

namespace EasyBlog.Core.Utilities.Results.Concrete;

public class DataResult<T> : IDataResult<T>
{
    public DataResult(
        ResultStatus resultStatus,
        string message = null,
        T data = default,
        Exception exception = null,
        IEnumerable<ValidationError> validationErrors = null)
    {
        ResultStatus = resultStatus;
        Message = message;
        Data = data;
        Exception = exception;
        ValidationErrors = validationErrors;
    }

    public DataResult(
        ResultStatus resultStatus,
        T data) : this(resultStatus, null, data, null, null) { }

    public T Data { get; }
    public ResultStatus ResultStatus { get; }
    public string Message { get; }
    public Exception Exception { get; }
    public IEnumerable<ValidationError> ValidationErrors { get; }
}
