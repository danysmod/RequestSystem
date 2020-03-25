namespace Domain.Statements
{
    using Domain.BaseEntity;
    using Domain.Statements.Status;
    using Domain.Statements.Status.ValueObjects;
    using Domain.Statements.ValueObjects;

    public class Statement : BaseEntity, IStatement
    {
        public StatementTitle Title { get; protected set; }

        public IStatementStatus CurrentStatus { get; protected set; }

        public StatementStatusCollection StatusHistory { get; protected set; }

        public Statement()
        {
            this.StatusHistory = new StatementStatusCollection();
        }

        public IStatementStatus ChangeStatus(
            IStatementFactory statementFactory,
            StatusCode statusCode,
            StatusComment statusComment)
        {
            var statementStatus = statementFactory.NewStatus(
                this,
                statusCode,
                statusComment);

            this.CurrentStatus = statementStatus;
            this.StatusHistory.Add(statementStatus);

            return statementStatus;
        }

        public void ChangeTitle(StatementTitle newTitle) => this.Title = newTitle;
    }
}
