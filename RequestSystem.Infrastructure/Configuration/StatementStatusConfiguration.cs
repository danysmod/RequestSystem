using Domain.BaseEntity;
using Domain.Statements.Status.ValueObjects;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StatementStatusConfiguration : IEntityTypeConfiguration<StatementStatus>
    {
        public void Configure(EntityTypeBuilder<StatementStatus> builder)
        {
            builder.ToTable("Statuses");

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.ToGuid(),
                    v => new PrimaryKey(v))
                .IsRequired();

            builder.Property(p => p.StatementId)
                .HasConversion(
                    v => v.ToGuid(),
                    v => new PrimaryKey(v))
                .IsRequired();

            builder.Property(p => p.Comment)
                .HasConversion(
                    v => v.ToString(),
                    v => new StatusComment(v))
                .IsRequired();
        }
    }
}
