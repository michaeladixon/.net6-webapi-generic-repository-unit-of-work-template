using Models;

namespace Logic.Repository.Generic.Interfaces;

public interface IUserRepository
{
    Task<IReadOnlyList<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(long id);
}
