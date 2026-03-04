using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.ProjectsDtos.StatusesDto;
using BackendTascly.Entities;

namespace BackendTascly.Profiles
{
    public class StatusProfile: Profile
    {
        public StatusProfile()
        {
            // Mapping between PTaskStatus and GetProject DTO (GetStatusDto from PTaskStatus)
            CreateMap<PTaskStatus, GetStatusDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.NextStatusId, src => src.MapFrom(x => x.NextStatusId))
                .ForMember(dest => dest.ProjectId, src => src.MapFrom(x => x.ProjectId));
            
        }
    }
}
