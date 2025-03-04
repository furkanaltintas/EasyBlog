using AutoMapper;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Service.Services.Concretes;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public UserRegistrationService(UserManager<AppUser> userManager, IRoleService roleService, IMapper mapper)
    {
        _userManager = userManager;
        _roleService = roleService;
        _mapper = mapper;
    }



    public async Task<IDataResult<IList<UserListDto>>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userListDtos = _mapper.Map<List<UserListDto>>(users);

        foreach (var user in userListDtos)
        {
            var findUser = await _userManager.FindByIdAsync(user.Id.ToString());
            var roles = string.Join(", ", await _userManager.GetRolesAsync(findUser));

            user.Role = roles;
        }

        return new DataResult<List<UserListDto>>(ResultStatus.Success, userListDtos);
    }

    public async Task<IDataResult<UserDto>> GetUserByIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return new DataResult<UserDto>(ResultStatus.Error, "Sistemde böyle bir kullanıcı bulunmamaktadır.");
        }

        var userDto = _mapper.Map<UserDto>(user);
        return new DataResult<UserDto>(ResultStatus.Success, userDto);
    }

    public async Task<IDataResult<UserUpdateDto>> GetUserByUpdateIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return new DataResult<UserUpdateDto>(ResultStatus.Error, "Böyle bir kullanıcı sistemde bulunmamaktadır");

        var userUpdateDto = _mapper.Map<UserUpdateDto>(user);

        var roles = await _roleService.GetRolesAsync();

        if (roles.ResultStatus == ResultStatus.Success)
            userUpdateDto.Roles = roles.Data;
        return new DataResult<UserUpdateDto>(ResultStatus.Success, userUpdateDto);
    }



    // Kullanıcı kaydını oluşturma
    public async Task<IResult> CreateUserAsync(UserAddDto userAddDto)
    {
        try
        {
            // E-posta doğrulama
            var existingUser = await _userManager.FindByEmailAsync(userAddDto.Email);
            if (existingUser != null)
                return new Result(ResultStatus.Error, "Bu e-posta adresi zaten kullanılıyor");

            var appUser = _mapper.Map<AppUser>(userAddDto);
            appUser.UserName = userAddDto.Email;

            if (string.IsNullOrEmpty(userAddDto.Password))
                return new Result(ResultStatus.Error, "Şifre boş olamaz");

            var result = await _userManager.CreateAsync(appUser, userAddDto.Password);
            if (result.Succeeded)
            {
                var role = await _roleService.GetByRoleIdAsync(userAddDto.RoleId);
                if (role == null)
                    return new Result(ResultStatus.Error, "Geçersiz rol");

                var roleResult = await _userManager.AddToRoleAsync(appUser, role);
                if (!roleResult.Succeeded)
                    return new Result(ResultStatus.Error, "Rol atama işlemi başarısız");

                return new Result(ResultStatus.Success, Messages.User.Add(userAddDto.Email));
            }

            return new Result(ResultStatus.Error, "Kullanıcı oluşturulurken bir hata oluştu.");
        }
        catch (Exception)
        {
            return new Result(ResultStatus.Error, "Beklenmeyen bir hata oluştu.");
        }
    }

    // Kullanıcıyı güncelleme
    public async Task<IDataResult<UserUpdateDto>> UpdateUserAsync(UserUpdateDto userUpdateDto, Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return new DataResult<UserUpdateDto>(ResultStatus.Error, "Böyle bir kullanıcı sistemde bulunmamaktadır");

        var userRole = string.Join(", ", await _userManager.GetRolesAsync(user));

        _mapper.Map(userUpdateDto, user);
        user.Id = userId;
        user.UserName = userUpdateDto.Email;
        user.SecurityStamp = Convert.ToString(Guid.NewGuid());
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            await _userManager.RemoveFromRoleAsync(user, userRole);
            var findRole = await _roleService.GetByRoleIdAsync(userUpdateDto.RoleId);
            await _userManager.AddToRoleAsync(user, findRole);
            return new DataResult<UserUpdateDto>(ResultStatus.Success, Messages.User.Update(user.Email!));
        }

        var roles = await _roleService.GetRolesAsync();
        userUpdateDto.Roles = roles.Data;
        return new DataResult<UserUpdateDto>(ResultStatus.Error, "Kullanıcı oluşturulurken bir hata oluştu.", userUpdateDto);
    }

    // Kullanıcının hesabını askıya alma
    public async Task<IResult> DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return new Result(ResultStatus.Success, Messages.User.Delete(user.Email));
        }

        return new Result(ResultStatus.Success, "Sistemde böyle bir kullanıcı bulunmamaktadır.");
    }
}