using Domain.BaseEntity;
using Domain.Statements;
using Domain.Statements.Status.ValueObjects;
using System;

namespace Infrastructure.Entities
{
    public class StatementStatus : Domain.Statements.Status.StatementStatus
    {
        public StatementStatus(
            IStatement statement, 
            StatusCode statusCode, 
            StatusComment statusComment)
        {
            this.Id = new PrimaryKey(Guid.NewGuid());
            this.StatementId = statement.Id;
            this.StatusCode = statusCode;
            this.Comment = statusComment;
        }

        protected StatementStatus()
        {

        }

        public PrimaryKey StatementId { get; protected set; }
    }
}
