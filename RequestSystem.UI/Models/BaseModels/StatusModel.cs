using Domain.Statements.Status;
using System;

namespace StatementSystem.UI.Models
{
    public class StatusModel
    {
        public Guid StatusId { get; set; }

        public Guid StatementId { get; set; }

        public string Comment { get; set; }

        public int StatusCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public StatusModel(IStatementStatus status)
        {
            if(status is StatementStatus statusEntity)
            {
                Comment = statusEntity.Comment.ToString();
                CreateDate = statusEntity.CreateDate;
                EditDate = statusEntity.EditDate;
                StatusCode = (int)statusEntity.StatusCode;
                StatusId = statusEntity.Id.ToGuid();
            }
        }

        public StatusModel() { }
    }
}
