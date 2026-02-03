namespace Data.Context.Entities;

/// <summary>
/// Example result class for stored procedure results.
/// Create your own result classes for your stored procedures.
/// </summary>
public class SomeStoredProcedureResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Groups { get; set; }
}
