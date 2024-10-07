using MediatR;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Domain.Entities.UserEntity;
using ReportSystem.Domain.IRepository.IUnitOfWork;

namespace ReportSystem.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserHundler : IRequestHandler<DeleteUserCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDTO _responseDTO;

        public DeleteUserHundler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user == null)
            {
                _responseDTO.Data = null;
                _responseDTO.ErrorMessage = "Use Not Found";
                _responseDTO.StatusCode = 200;
                return _responseDTO;
            }

            user.State = Domain.Common.Enums.State.Deleted;
            _unitOfWork.Repository<User>().Update(user);

            _responseDTO.ErrorMessage = "Can't Delete User";
            _responseDTO.StatusCode = 200;

            if (await _unitOfWork.SaveChangesAsync() == 0)
                return _responseDTO;
            return new ResponseDTO();
        }
    }
}
