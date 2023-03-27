using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceWScheduler.Models.Models;

namespace SpaceWScheduler.Models.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasKey(e => e.ID);
            builder
                .Property(e => e.ID)
                .IsRequired();
            builder
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(e => e.StartTime)
                .IsRequired();
            builder
                .Property(e => e.EndTime)
                .IsRequired();
            builder
                .HasOne(e => e.Schedule)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.ScheduleId); // Foreign key in the one to many relationship
        }
    }
}
