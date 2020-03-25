using Domain.BaseEntity;
using Domain.Statements.ValueObjects;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StatementConfiguration : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.ToTable("Statements");

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.ToGuid(),
                    v => new PrimaryKey(v))
                .IsRequired();

            builder.Property(p => p.Title)
                .HasConversion(
                    v => v.ToString(),
                    v => new StatementTitle(v))
                .IsRequired();

            builder.Property(p => p.CurrentStatusId)
                .HasConversion(
                    v => v.ToGuid(),
                    v => new PrimaryKey(v));

            builder.Ignore(p => p.StatusHistory)
                .Ignore(p => p.CurrentStatus);
        }
    }
}
