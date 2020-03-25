namespace Domain.Statements.Status
{
    using Domain.BaseEntity;
    using Domain.Statements.Status.ValueObjects;

    public class StatementStatus : BaseEntity, IStatementStatus
    {
        public StatusCode StatusCode { get; protected set; }

        public StatusComment Comment { get; protected set; }
    }
}
