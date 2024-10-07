using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Domain.Entities.UserEntity;

namespace ReportSystem.Application.Features.Users.Queries.LoggedInUser
{
    public class LoggedInUserQueryHandler : IRequestHandler<LoggedInUserQuery, ResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userRepository;
        private readonly ResponseDTO _responseDTO;
        public long _loggedInUserId;

        public LoggedInUserQueryHandler(IMapper mapper,
            UserManager<User> userRepository,
            IHttpContextAccessor _httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository;
            _responseDTO = new ResponseDTO();
            _loggedInUserId = long.Parse(_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "userLoginId").SingleOrDefault()?.Value);
        }

        public async Task<ResponseDTO> Handle(LoggedInUserQuery request, CancellationToken cancellationToken)
        {

            var entityObj = await _userRepository.FindByIdAsync(_loggedInUserId.ToString());

            
            if (entityObj == null)
            {
                _responseDTO.Data = null;
                _responseDTO.ErrorMessage = "Use Not Found";
                _responseDTO.StatusCode = 200;
                return _responseDTO;
            }
            var user = _mapper.Map<UserDTO>(entityObj);

            _responseDTO.Data = user;
            _responseDTO.StatusCode = 200;

            return _responseDTO;
        }
    }
}
