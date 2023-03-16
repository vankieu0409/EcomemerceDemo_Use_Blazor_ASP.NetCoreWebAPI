using EF.Support.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace ASMC6P.Shared.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    public bool IsDeleted { get; set; }
    public string DisplayName { get; set; }
    public string Decriptions { get; set; }
    public string Image { get; set; }
}