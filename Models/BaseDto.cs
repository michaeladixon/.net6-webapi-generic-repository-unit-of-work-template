namespace Models;

public abstract class BaseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
