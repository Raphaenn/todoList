using AutoMapper;
using TodoList.Models;
using TodoList.Models.Dtos;
using TodoList.Models.Entity;

namespace TodoList.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskModel, TaskDto>().ForMember(x => x.Nome, opt => opt.MapFrom(x => x.Name)).ForMember(x => x.Descrição, opt => opt.MapFrom(x => x.Description));
        
        // CreateMap<UserModel, UserDto>();
    }
}

// If a model has a field with a diferent name that DTO, so we can ForMember(x => x.Name, opt => opt.MapFrom(x => x.Fullname) ) 