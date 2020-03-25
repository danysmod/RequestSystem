using Domain.BaseEntity;
using Domain.Statements.Status;
using Domain.Statements.ValueObjects;
using System;
using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class Statement : Domain.Statements.Statement
    {
        public Statement(StatementTitle requestTitle)
        {
            this.Id = new PrimaryKey(Guid.NewGuid());
            this.Title = requestTitle;
        }

        protected Statement()
        {

        }

        public PrimaryKey CurrentStatusId { get; set; }

        public void LoadStatuses(IList<StatementStatus> statementStatuses)
        {
            this.StatusHistory = new StatementStatusCollection();
            this.StatusHistory.Add(statementStatuses);
        }

        public void LoadCurrentStatus(StatementStatus statementStatus)
        {
            base.CurrentStatus = statementStatus;
        }
    }
}
