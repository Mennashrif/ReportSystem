using AutoMapper;
using MediatR;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Domain.Entities.UserEntity;
using ReportSystem.Domain.IRepository.IUnitOfWork;

namespace ReportSystem.Application.Features.Users.Commands.EditUser
{
    public class EditUserHundler : IRequestHandler<EditUserCommand, ResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDTO _responseDTO;

        public EditUserHundler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

            Map(user, request);
            _unitOfWork.Repository<User>().Update(user);

            _responseDTO.ErrorMessage = "Can't Edit User";
            _responseDTO.StatusCode = 200;

            if (await _unitOfWork.SaveChangesAsync() == 0)
                return _responseDTO;
            return new ResponseDTO();
        }

        public void Map(User user, EditUserCommand editUserCommand)
        {
            user.FullName = editUserCommand.FullName;
            user.Email = editUserCommand.Email;
            user.Birthdate = editUserCommand.Birthdate;
            user.PhoneNumber = editUserCommand.PhoneNumber;
            user.JobTitle = editUserCommand.JobTitle;
        }
    }
}
