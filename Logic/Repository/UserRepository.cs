using AutoMapper;
using Data.Entities.Context;
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
        private readonly IMapper _mapper;

        public UserRepository(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<UserDto> AddAsync(UserDto t)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserDto>> FindAllAsync(Expression<Func<UserDto, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> FindAsync(Expression<Func<UserDto, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserDto>> FindByAsync(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Repository<USER>().GetAllAsync();
            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public IQueryable<UserDto> GetAllIncluding(params Expression<Func<UserDto, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateAsync(UserDto t, object key)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserRepository : IGenericRepository<UserDto>
    {
        //Make custom functions available here specific the the Repo.
    }
}
