using Domain.BaseEntity;
using Domain.Statements;
using Domain.Statements.Status;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Statement = Infrastructure.Entities.Statement;
using StatementStatus = Infrastructure.Entities.StatementStatus;

namespace Infrastructure.Repositories
{
    public class StatementRepository : IStatementRepository
    {
        private readonly DataContext context;

        public StatementRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddStatement(IStatement statement)
        {
            await this.context.Statements.AddAsync((Statement)statement);
        }

        public async Task<IStatement> GetStatement(PrimaryKey primaryKey)
        {
            var statement = await context.Statements
                .Where(x => x.Id.Equals(primaryKey))
                .SingleOrDefaultAsync();

            if(statement is null)
            {
                throw new Exception("");
            }

            var statusHistory = await context.StatementStatuses
                .Where(x => x.StatementId.Equals(primaryKey))
                .ToListAsync();

            var currentStatus = await context.StatementStatuses
                .Where(x => x.Id.Equals(statement.CurrentStatusId))
                .SingleOrDefaultAsync();

            statement.LoadCurrentStatus(currentStatus);
            statement.LoadStatuses(statusHistory);

            return statement;
         }

        public async Task<IEnumerable<IStatement>> GetAll()
        {
            var statements = await this.context.Statements
                .ToListAsync();

            foreach(var statement in statements)
            {
                var currentStatuses = await context.StatementStatuses
                    .Where(p => p.StatementId.Equals(statement.Id))
                    .ToListAsync();

                statement.LoadStatuses(currentStatuses);

                var currentStatus = await context.StatementStatuses
                    .Where(p => p.Id.Equals(statement.CurrentStatusId))
                    .SingleOrDefaultAsync();

                statement.LoadCurrentStatus(currentStatus);
            }

            return statements;
        }

        public async Task Update(IStatement statement, IStatementStatus statementStatus)
        {
            await this.context.StatementStatuses.AddAsync((StatementStatus)statementStatus);
            ((Statement)statement).CurrentStatusId = statementStatus.Id;
        }

        public async Task DeleteStatement(IStatement statement)
        {
            var deleteStateSQL =
                @"DELETE FROM Statements WHERE Id = @id
                    DELETE FROM Statuses WHERE StatementId = @id";

            var id = new SqlParameter("@Id", statement.Id.ToGuid());

            int affectedRows = await this.context.Database
                .ExecuteSqlRawAsync(deleteStateSQL, id);
        }
    }
}
