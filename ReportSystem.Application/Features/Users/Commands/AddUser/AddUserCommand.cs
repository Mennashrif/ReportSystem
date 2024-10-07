using MediatR;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommand:IRequest<ResponseDTO>
    {
        public string Email { get; set; } = "";
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? Birthdate { get; set; }
    }
}
