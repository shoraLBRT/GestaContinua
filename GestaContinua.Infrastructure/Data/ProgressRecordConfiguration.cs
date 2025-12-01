using GestaContinua.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaContinua.Infrastructure.Data
{
    public class ProgressRecordConfiguration : IEntityTypeConfiguration<ProgressRecord>
    {
        public void Configure(EntityTypeBuilder<ProgressRecord> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ValueJson).HasMaxLength(4000);
        }
    }
}