using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.TaskDtos;
using BackendTascly.Entities;

namespace BackendTascly.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // Mapping between Task and GetTaskDTO (GetTask from Task)
            CreateMap<PTask, GetTask>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(dest => dest.DueDate, src => src.MapFrom(x => x.DueDate))
                .ForMember(dest => dest.CreationDate, src => src.MapFrom(x => x.CreationDate))
                .ForMember(dest => dest.CompletionDate, src => src.MapFrom(x => x.CompletionDate))
                .ForMember(dest => dest.LastModifiedDate, src => src.MapFrom(x => x.LastModifiedDate))
                .ForMember(dest => dest.StatusName, src => src.MapFrom(x => x.Status.Name))
                .ForMember(dest => dest.ImportanceName, src => src.MapFrom(x => x.Importance.Name))
                .ForMember(dest => dest.ProjectId, src => src.MapFrom(x => x.ProjectId))
                .ForMember(dest => dest.AuthorId, src => src.MapFrom(x => x.AuthorId))
                .ForMember(dest => dest.AssigneeId, src => src.MapFrom(x => x.AssigneeId));

            // Mapping between PostProject DTO and Project (Project from PostProject)
            CreateMap<PostProject, Project>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description));
        }
    }
}
