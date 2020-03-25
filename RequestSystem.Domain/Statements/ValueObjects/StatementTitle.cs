using System;

namespace Domain.Statements.ValueObjects
{
    public readonly struct StatementTitle
    {
        private readonly string statementTitle;

        public StatementTitle(string statementTitle)
        {
            if(string.IsNullOrEmpty(statementTitle))
            {
                throw new Exception("StatementTitle cannot be null or empty");
            }

            this.statementTitle = statementTitle;
        }

        public override string ToString() => this.statementTitle;
    }
}
