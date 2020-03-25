using Domain.BaseEntity;
using Domain.Statements.Status;
using Domain.Statements.Status.ValueObjects;
using Domain.Statements.ValueObjects;

namespace Domain.Statements
{
    public interface IStatement
    {
        PrimaryKey Id { get; }

        IStatementStatus CurrentStatus { get; }

        IStatementStatus ChangeStatus(
            IStatementFactory requestFactory,
            StatusCode statusCode,
            StatusComment statusComment);

        void ChangeTitle(StatementTitle newTitle);
    }
}
