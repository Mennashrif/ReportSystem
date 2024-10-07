using MediatR;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.Application.Features.Account.Queries.Login
{
    public record LoginQuery:IRequest<ResponseDTO>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
