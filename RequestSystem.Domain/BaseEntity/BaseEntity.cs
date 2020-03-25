using System;

namespace Domain.BaseEntity
{
    public abstract class BaseEntity
    {
        public PrimaryKey Id { get; protected set; }

        public DateTime CreateDate { get; set; }

        public DateTime? EditDate { get; set; }
    }
}
