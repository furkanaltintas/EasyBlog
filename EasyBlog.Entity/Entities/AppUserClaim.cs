using EasyBlog.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EasyBlog.Entity.Entities;

public class AppUserClaim : IdentityUserClaim<Guid>, IEntityBase
{
}