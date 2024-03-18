using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository.Generic.Interfaces
{
    public interface IUserRepository //: IGenericRepository<UserDto> <-- you can force implement all of the generic interfaces too.
    {
        //Add the functions you need your repo to have here.
         Task<ICollection<UserDto>> GetAllAsync();
    }
}
