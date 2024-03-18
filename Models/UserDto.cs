using Data.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserDto : BaseDto
    {
        public List<GroupDto> Groups { get; set; } = new();

        public static explicit operator UserDto(USER user)
        {
            return new UserDto
            {
                Id = user.id,
                Name = user.name,
                Groups = user.groups.Split(',').Select(x => new GroupDto { Name = x }).ToList()
            };
        }
    }
}
