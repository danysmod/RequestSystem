using System;

namespace Domain.Statements.Status.ValueObjects
{
    public readonly struct StatusComment
    {
        private readonly string statusComment;

        public StatusComment(string statusComment)
        {
            if(string.IsNullOrEmpty(statusComment))
            {
                throw new Exception("StatusComment cannot be null or empty");
            }

            this.statusComment = statusComment;
        }

        public override string ToString() => this.statusComment;
    }
}
