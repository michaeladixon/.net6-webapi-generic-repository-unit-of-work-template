namespace Data.Context.Entities;

public partial class ApplicationDbContext
{
    private IApplicationDbContextProcedures? _procedures;

    public IApplicationDbContextProcedures Procedures => _procedures ??= new ApplicationDbContextProcedures(this);

    public IApplicationDbContextProcedures GetProcedures() => Procedures;
}

/// <summary>
/// Implementation of stored procedures.
/// Add your stored procedure implementations here.
/// </summary>
public class ApplicationDbContextProcedures(ApplicationDbContext context) : IApplicationDbContextProcedures
{
    // Example implementation:
    // public async Task<List<SomeResult>> GetSomeDataAsync(int id, CancellationToken cancellationToken = default)
    // {
    //     return await context.SqlQueryAsync<SomeResult>(
    //         "SELECT * FROM SomeTable WHERE Id = {0}",
    //         [id],
    //         cancellationToken);
    // }
}
