using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Users;

namespace EasyBlog.Service.Services.Abstractions;

public interface IUserProfileService
{
    Task<IResult> ChangeUserAsync(UserProfileDto userProfileDto);
    Task<IDataResult<UserProfileDto>> GetUserAsync();
}