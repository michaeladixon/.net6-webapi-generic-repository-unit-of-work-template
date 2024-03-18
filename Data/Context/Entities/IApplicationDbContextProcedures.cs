using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context.Entities
{
    public partial interface IApplicationDbContextProcedures
    {
        //puts all the stored proc methods with their params here in the interface.
        Task<int> SomeStoredProcedureMethod(int? example, string parameters, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
