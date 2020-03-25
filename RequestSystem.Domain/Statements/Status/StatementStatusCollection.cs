using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Statements.Status
{
    public sealed class StatementStatusCollection
    {
        private readonly IList<IStatementStatus> statusCollection;

        public StatementStatusCollection()
        {
            this.statusCollection = new List<IStatementStatus>();
        }

        public void Add<T>(IEnumerable<T> statementStatuses)
            where T : IStatementStatus
        {
            if(statementStatuses is null)
            {
                throw new ArgumentNullException(nameof(statementStatuses));
            }

            foreach(var status in statementStatuses)
            {
                if(status is null)
                {
                    throw new ArgumentNullException(nameof(statementStatuses));
                }

                this.Add(status);
            }
        }

        public void Add(IStatementStatus statementStatus) => this.statusCollection.Add(statementStatus);

        public IReadOnlyCollection<IStatementStatus> GetCollection() 
            => new ReadOnlyCollection<IStatementStatus>(this.statusCollection);
    }
}
