using Data.Context.Entities;
using Logic.Attributes;
using Logic.IUnitOfWork;
using Logic.Repository.Generic.Interfaces;
using Models;
using System.Linq.Expressions;

namespace Logic.Repository
{
    [ServiceImplementation(typeof(IUserRepository))]
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<USER> _userRepo;

        public UserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.Repository<USER>();

        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(user => (UserDto)user).ToList();
        }

    }


}
