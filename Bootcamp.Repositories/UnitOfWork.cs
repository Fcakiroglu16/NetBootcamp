using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.ApplicationService;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bootcamp.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        //begin transaction method
        public void BeginTransaction()
        {
            _transaction = context.Database.BeginTransaction();
        }

        public Task? TransactionCommitAsync()
        {
            return _transaction?.CommitAsync();
        }

        public Task? TransactionRollbackAsync()
        {
            return _transaction?.RollbackAsync();
        }

        public Task<int> CommitAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}