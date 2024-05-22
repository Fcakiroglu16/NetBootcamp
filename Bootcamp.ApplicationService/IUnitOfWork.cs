using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.ApplicationService
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task? TransactionCommitAsync();
        Task? TransactionRollbackAsync();
        Task<int> CommitAsync();
    }
}