using AutoMapper;
using TodoList.Models;
using TodoList.Models.Dtos;

namespace TodoList.Mapping;

public class AutoMapperUsers : Profile
{
    public AutoMapperUsers()
    {
        CreateMap<UserModel, UserDto>();
    }
}