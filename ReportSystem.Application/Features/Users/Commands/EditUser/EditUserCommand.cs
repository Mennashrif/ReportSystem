using MediatR;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<ResponseDTO>
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? Birthdate { get; set; }
    }
}
