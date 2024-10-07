using AutoMapper;
using MediatR;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Domain.Entities.UserEntity;
using ReportSystem.Domain.IRepository.IUnitOfWork;

namespace ReportSystem.Application.Features.Users.Commands.AddUser
{
    public class AddUserHundler : IRequestHandler<AddUserCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDTO;

        public AddUserHundler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var addedUser = _mapper.Map<User>(request);
            await _unitOfWork.Repository<User>().AddAsync(addedUser);

            _responseDTO.ErrorMessage = "Can't Add User";
            _responseDTO.StatusCode = 200;

            if (await _unitOfWork.SaveChangesAsync() == 0)
                return _responseDTO;
            return new ResponseDTO();
        }
    }
}
