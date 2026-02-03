using Data.Context.Entities;
using Logic.IUnitOfWork;
using Logic.Repository.Generic.Interfaces;
using Models;

namespace Logic.Repository;

public class UserRepository(IUnitOfWork unitOfWork) : IUserRepository
{
    private readonly IGenericRepository<USER> _userRepo = unitOfWork.Repository<USER>();

    public async Task<IReadOnlyList<UserDto>> GetAllAsync()
    {
        var users = await _userRepo.GetAllAsync();
        return users.Select(UserDto.FromEntity).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        return user is null ? null : UserDto.FromEntity(user);
    }
}
