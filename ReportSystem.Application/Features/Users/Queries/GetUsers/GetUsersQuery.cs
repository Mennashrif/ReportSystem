using MediatR;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery:IRequest<ResponseDTO>
    {
    }
}
