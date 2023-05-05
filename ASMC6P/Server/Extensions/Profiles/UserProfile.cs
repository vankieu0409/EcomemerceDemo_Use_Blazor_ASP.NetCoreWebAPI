using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;
using ASMC6P.Shared.ViewModels;

using AutoMapper;

namespace ASMC6P.Server.Extensions.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, UserEntity>().ReverseMap();
        CreateMap<UpdateProfileVM, UserEntity>().ReverseMap();
        CreateMap<ProductAdminDto, ProductEntity>().ReverseMap();
    }
}