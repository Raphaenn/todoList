using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models;
using TodoList.Models.Entity;

namespace TodoList.Data.Maps;

public class TaskMap : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.UserId);

        builder.HasOne(x => x.User);
    }
}