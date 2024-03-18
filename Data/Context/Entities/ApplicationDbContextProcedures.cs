using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Context.Entities
{
    public partial class ApplicationDbContext
    {
        private IApplicationDbContextProcedures _procs;

        public virtual IApplicationDbContextProcedures Procedures => _procs ??= new ApplicationDbContextProcedures(this);


        public IApplicationDbContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SomeStoredProcedureResult>().HasNoKey().ToView(null);
        }
    }
    public partial class ApplicationDbContextProcedures : IApplicationDbContextProcedures
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextProcedures(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> SomeStoredProcedureMethod(int? example, string parameter, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "example",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "parameter",
                    Size = 10,
                    Value = parameter ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Char,
                },
                parameterreturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[spclaw_CategoryInUse] @CatCode", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
