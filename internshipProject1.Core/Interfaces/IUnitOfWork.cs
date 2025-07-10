using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Core.Interfaces
{
    internal interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRouteRepository Routes { get; }
        ITripRepository Trips { get; }
        IStopRepository Stops { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
