using AutoMapper;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;

namespace BackendTascly.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            // Mapping between User and GetUserDto (GetUserDto from User)
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Username, src => src.MapFrom(x => x.Username))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.IsSuperAdmin, src => src.MapFrom(x => x.IsSuperAdmin));

            // Mapping between User and GetUserProfile (GetUserProfile from User)
            CreateMap<User, GetUserProfile>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Username))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.IsAdmin, src => src.MapFrom(x => x.IsSuperAdmin))
                .ForMember(dest => dest.OrganizationName, src => src.MapFrom(x => x.Organization.Name));

            // Mapping between GetUserProfile and User (User from GetUserProfile)
            CreateMap<GetUserProfile, User>()
                .ForMember(dest => dest.Username, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName));
        }
    }
}
