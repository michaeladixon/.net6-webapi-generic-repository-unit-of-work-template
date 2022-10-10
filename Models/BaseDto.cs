using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class UserDto : BaseDto
    {
        public List<GroupDto> Groups { get; } = new();
    }

    public class GroupDto : BaseDto
    {


    }
}
