using Domain.Statements.Status;
using Domain.Statements.Status.ValueObjects;
using Domain.Statements.ValueObjects;

namespace Domain.Statements
{
    public interface IStatementFactory
    {
        IStatement NewStatement(StatementTitle title);

        IStatementStatus NewStatus(
            IStatement statement, 
            StatusCode statusCode, 
            StatusComment statusComment);
    }
}
