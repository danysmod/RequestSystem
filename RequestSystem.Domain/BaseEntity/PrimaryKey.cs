using System;

namespace Domain.BaseEntity
{
    public readonly struct PrimaryKey : IEquatable<PrimaryKey>
    {
        private readonly Guid primaryKey;

        public PrimaryKey(Guid primaryKey)
        {
            if(Guid.Empty == primaryKey)
            {
                throw new Exception("PrimaryKey cannot be empty");
            }

            this.primaryKey = primaryKey;
        }

        public bool Equals(PrimaryKey other) => this.primaryKey == other.primaryKey;

        public override string ToString() => this.primaryKey.ToString();

        public Guid ToGuid() => this.primaryKey;

        public override bool Equals(object obj)
        {
            if(obj is PrimaryKey primaryKey)
            {
                return this.Equals(primaryKey);
            }

            return false;
        }

        public override int GetHashCode() => this.primaryKey.GetHashCode();
    }
}
