using Data.Context.Entities;

namespace Models;

public class GroupDto : BaseDto
{
    public static GroupDto FromEntity(GROUP group) => new()
    {
        Id = group.Id,
        Name = group.Name
    };
}
