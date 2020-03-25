using Domain.Statements;
using System;
using System.Collections.Generic;

namespace StatementSystem.UI.Models
{
    public class StatementModel
    {
        public Guid StatementId { get; set; }

        public string StatementTitle { get; set; }

        public StatusModel CurrentStatus { get; set; }

        public List<StatusModel> StatusHistory { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public StatementModel(IStatement statement)
        {
            if(statement is Statement statementEntity)
            {
                var statusHistory = new List<StatusModel>();
                foreach (var status in statementEntity.StatusHistory.GetCollection())
                {
                    statusHistory.Add(new StatusModel(status));
                }

                var currentStatus = new StatusModel(statement.CurrentStatus);

                CreateDate = statementEntity.CreateDate;
                EditDate = statementEntity.EditDate;
                CurrentStatus = currentStatus;
                StatementId = statementEntity.Id.ToGuid();
                StatementTitle = statementEntity.Title.ToString();
                StatusHistory = statusHistory;
            }
        }

        public StatementModel() { }
    }
}
