using Data.Context.Entities;

namespace Models;

public class UserDto : BaseDto
{
    public List<GroupDto> Groups { get; set; } = [];

    public static UserDto FromEntity(USER user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Groups = string.IsNullOrEmpty(user.Groups)
            ? []
            : user.Groups.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(name => new GroupDto { Name = name.Trim() })
                .ToList()
    };

    public static explicit operator UserDto(USER user) => FromEntity(user);
}
