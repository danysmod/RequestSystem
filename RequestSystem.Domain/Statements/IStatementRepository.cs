using Domain.BaseEntity;
using Domain.Statements.Status;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Statements
{
    public interface IStatementRepository
    {
        Task<IStatement> GetStatement(PrimaryKey primaryKey);

        Task<IEnumerable<IStatement>> GetAll();

        Task AddStatement(IStatement statement);

        Task Update(IStatement statement, IStatementStatus statementStatus);

        Task DeleteStatement(IStatement statement);
    }
}
