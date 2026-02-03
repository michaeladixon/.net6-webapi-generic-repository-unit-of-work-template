using Microsoft.EntityFrameworkCore;

namespace Data.Context.Entities;

public static class DbContextExtensions
{
    public static async Task<List<T>> SqlQueryAsync<T>(
        this DbContext db,
        string sql,
        object[]? parameters = null,
        CancellationToken cancellationToken = default) where T : class
    {
        parameters ??= [];

        if (typeof(T).GetProperties().Length != 0)
        {
            return await db.Set<T>().FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
        }

        await db.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        return [];
    }
}

public class OutputParameter<TValue>
{
    private bool _valueSet;
    private TValue? _value;

    public TValue Value
    {
        get
        {
            if (!_valueSet)
                throw new InvalidOperationException("Value not set.");

            return _value!;
        }
    }

    public void SetValue(object? value)
    {
        _valueSet = true;
        _value = value is null || Convert.IsDBNull(value) ? default : (TValue)value;
    }
}
