using AutoMapper;
using ReportSystem.Application.Features.Users.Commands.AddUser;
using ReportSystem.Application.Features.Users.Commands.EditUser;
using ReportSystem.Application.Features.Users.Queries.LoggedInUser;
using ReportSystem.Domain.Entities.UserEntity;

namespace ReportSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<AddUserCommand, User>();
            CreateMap<EditUserCommand, User>();
        }

    }
}
