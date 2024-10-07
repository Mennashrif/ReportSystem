using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Application.Features.Users.Queries.LoggedInUser;
using ReportSystem.Domain.Entities.UserEntity;
using ReportSystem.Domain.IRepository.IUnitOfWork;

namespace ReportSystem.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersHundler : IRequestHandler<GetUsersQuery, ResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDTO _responseDTO;

        public GetUsersHundler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDTO = new ResponseDTO();
            _mapper = mapper;

        }
        public async Task<ResponseDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users  = await _unitOfWork.Repository<User>().FindAllNoTracking(x=>x.State == Domain.Common.Enums.State.NotDeleted).ToListAsync();

            var user = _mapper.Map<List<UserDTO>>(users);

            _responseDTO.Data = user;
            _responseDTO.StatusCode = 200;

            return _responseDTO;
        }
    }
}
