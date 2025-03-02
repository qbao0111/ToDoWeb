using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoWeb.Domains.Entities;

namespace ToDoWeb.Infrastructures.DatabaseMapping
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.StartDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
