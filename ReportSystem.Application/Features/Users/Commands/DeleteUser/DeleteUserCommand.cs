using MediatR;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand:IRequest<ResponseDTO>
    {
        public int UserId { get; set; }
    }
}
