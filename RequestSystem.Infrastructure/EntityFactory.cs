using Domain.Statements;
using Domain.Statements.Status;
using Domain.Statements.Status.ValueObjects;
using Domain.Statements.ValueObjects;

namespace Infrastructure
{
    public class EntityFactory : IStatementFactory
    {
        public IStatement NewStatement(StatementTitle title) => new Entities.Statement(title);

        public IStatementStatus NewStatus(
            IStatement statement,
            StatusCode statusCode,
            StatusComment statusComment) => new Entities.StatementStatus(statement, statusCode, statusComment);
    }
}
