using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Users;

namespace EasyBlog.Service.Services.Abstractions;

public interface IUserRegistrationService
{
    Task<IResult> DeleteUserAsync(Guid userId);
    Task<IResult> CreateUserAsync(UserAddDto userAddDto);
    Task<IDataResult<UserUpdateDto>> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid userId);


    Task<IDataResult<UserUpdateDto>> GetUserByUpdateIdAsync(Guid userId);
    Task<IDataResult<UserDto>> GetUserByIdAsync(Guid userId);
    Task<IDataResult<IList<UserListDto>>> GetAllUsersAsync();

}