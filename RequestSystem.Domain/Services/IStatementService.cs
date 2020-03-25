using Domain.BaseEntity;
using Domain.Statements;
using Domain.Statements.Status.ValueObjects;
using Domain.Statements.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStatementService
    {
        Task<IStatement> CreateStatement(StatementTitle statementTitle);

        Task<IStatement> ChangeStatementStatus(
            PrimaryKey statementId,
            StatusCode statusCode,
            StatusComment statusComment);

        Task DeleteStatement(PrimaryKey statementId);

        Task<IStatement> ChangeStatementTitle(PrimaryKey statementId, StatementTitle newTitle);
    }
}
