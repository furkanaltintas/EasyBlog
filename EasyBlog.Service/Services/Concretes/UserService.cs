using Autofac.Extras.DynamicProxy;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Service.Aspects;
using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Concretes;

[Intercept(typeof(ValidationAspect))]
[Intercept(typeof(CacheAspect))]
public class UserService : IUserService
{
    private readonly IUserProfileService _userProfileService;
    private readonly IUserRegistrationService _userRegistrationService;

    public UserService(IUserProfileService userProfileService, IUserRegistrationService userRegistrationService)
    {
        _userProfileService = userProfileService;
        _userRegistrationService = userRegistrationService;
    }

    public async Task<IDataResult<IList<UserListDto>>> GetUsersAsync()
        => await _userRegistrationService.GetAllUsersAsync();


    public async Task<IDataResult<UserProfileDto>> GetUserAsync()
        => await _userProfileService.GetUserAsync();



    public async Task<IDataResult<UserUpdateDto>> GetUserByUpdateGuidAsync(Guid userId) 
        => await _userRegistrationService.GetUserByUpdateIdAsync(userId);


    public async Task<IResult> CreateUserAsync(UserAddDto userAddDto) 
        => await _userRegistrationService.CreateUserAsync(userAddDto);


    public async Task<IResult> ChangeUserAsync(UserProfileDto userProfileDto) 
        => await _userProfileService.ChangeUserAsync(userProfileDto);
  

    public async Task<IDataResult<UserUpdateDto>> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid userId) => await _userRegistrationService.UpdateUserAsync(userUpdateDto, userId);


    public async Task<IResult> DeleteUserAsync(Guid userId) 
        => await _userRegistrationService.DeleteUserAsync(userId);

}