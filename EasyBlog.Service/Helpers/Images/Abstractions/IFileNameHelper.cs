namespace EasyBlog.Service.Helpers.Images.Abstractions;

public interface IFileNameHelper
{
    string GenerateFileName(string name, string originalFileName);

    string ReplaceInvalidChars(string fileName);
}