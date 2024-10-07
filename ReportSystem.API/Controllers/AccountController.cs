using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportSystem.Application.Features.Account.Queries.Login;
using ReportSystem.Application.Features.Common.DTO;

namespace ReportSystem.API.Controllers
{
    public class AccountController(IMediator mediator) : ApiBaseController
    {
        private readonly IMediator _mediator = mediator;
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ResponseDTO> Login([FromBody] LoginQuery command)
        {
            return await _mediator.Send(command);
        }

    }
}