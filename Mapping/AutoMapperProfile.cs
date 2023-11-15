using AutoMapper;
using TodoList.Models.Dtos;
using TodoList.Models.Entity;

namespace TodoList.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskModel, TaskDto>();
    }
}

// If a model has a field with a diferent name that DTO, so we can ForMember(x => x.Name, opt => opt.MapFrom(x => x.Fullname) ) 