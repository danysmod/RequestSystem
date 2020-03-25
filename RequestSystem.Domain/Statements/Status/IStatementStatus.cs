using Domain.BaseEntity;
using Domain.Statements.Status.ValueObjects;

namespace Domain.Statements.Status
{
    public interface IStatementStatus
    {
        PrimaryKey Id { get; }

        StatusCode StatusCode { get; }

        StatusComment Comment { get; }
    }
}
