using EasyBlog.Core.Entities.Concrete;
using EasyBlog.Core.Utilities.Results.ComplexTypes;

namespace EasyBlog.Core.Utilities.Results.Abstract;

public interface IResult
{
    public ResultStatus ResultStatus { get; }
    public string Message { get; }
    public Exception Exception { get; }
    public IEnumerable<ValidationError> ValidationErrors { get; }
}