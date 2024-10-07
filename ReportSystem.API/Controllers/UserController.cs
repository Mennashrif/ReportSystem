using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Application.Features.Users.Commands.AddUser;
using ReportSystem.Application.Features.Users.Commands.DeleteUser;
using ReportSystem.Application.Features.Users.Commands.EditUser;
using ReportSystem.Application.Features.Users.Queries.GetUsers;
using ReportSystem.Application.Features.Users.Queries.LoggedInUser;

namespace ReportSystem.API.Controllers
{

    // [AllowAnonymous]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController(IMediator mediator) : ApiBaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("Profile")]
        public async Task<ResponseDTO> GetProfile()
        {
            return await _mediator.Send(new LoggedInUserQuery() { });
        }

        [HttpGet]
        [Route("Users")]
        public async Task<ResponseDTO> Users()
        {
            return await _mediator.Send(new GetUsersQuery() { });
        }

        [HttpPost]
        [Route("Users")]
        public async Task<ResponseDTO> Users([FromBody] AddUserCommand addUserCommand)
        {
            return await _mediator.Send(addUserCommand);
        }

        [HttpPut]
        [Route("Users")]
        public async Task<ResponseDTO> Users([FromBody] EditUserCommand editUserCommand)
        {
            return await _mediator.Send(editUserCommand);
        }
        [HttpDelete]
        [Route("Users/{userId}")]
        public async Task<ResponseDTO> Users(int userId)
        {
            return await _mediator.Send(new DeleteUserCommand() { UserId = userId });
        }
    }

}
