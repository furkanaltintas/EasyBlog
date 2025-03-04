using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Users;

namespace EasyBlog.Service.Services.Abstractions;

public interface IUserService
{
    Task<IDataResult<IList<UserListDto>>> GetUsersAsync();
    Task<IDataResult<UserProfileDto>> GetUserAsync();
    Task<IDataResult<UserUpdateDto>> GetUserByUpdateGuidAsync(Guid userId);
    Task<IResult> CreateUserAsync(UserAddDto userAddDto);
    Task<IDataResult<UserUpdateDto>> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid userId);
    Task<IResult> ChangeUserAsync(UserProfileDto userProfileDto);
    Task<IResult> DeleteUserAsync(Guid userId);
}