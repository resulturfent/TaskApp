using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entity;

namespace TaskManagement.DataLayer.Configurations
{
    public  class AppTaskConfiguration : IEntityTypeConfiguration<AppTask>
    {
        public void Configure(EntityTypeBuilder<AppTask> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(p=>p.Id).UseIdentityColumn();
            builder.Property(p=>p.Title).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(6000);
            builder.Property(p => p.TaskStatus).IsRequired();
            builder.Property(p => p.CreatedByUserId).IsRequired();
            builder.Property(p => p.AssignedToUserId).IsRequired(true);

            //bağlantılar 
            builder.HasOne(p=>p.AssignedToUser).WithMany(k=>k.AssignedUserTasks).HasForeignKey(p=>p.AssignedToUserId);
            builder.HasOne(p=>p.CreatedByUser).WithMany(k=>k.CreatedUserTasks).HasForeignKey(p=>p.CreatedByUserId);


        }
        #region My Code

        //public static void Configure(ModelBuilder modelBuilder)
        //{
        //    // AppTask için gerekli ilişkiler
        //    //modelBuilder.Entity<AppTask>()
        //    //    .HasKey(t => t.Id);

        //    //modelBuilder.Entity<AppTask>()
        //    //    .Property(t => t.Title)
        //    //    .IsRequired();

        //    //modelBuilder.Entity<AppTask>()
        //    //    .Property(t => t.TaskStatus)
        //    //    .IsRequired();

        //    //modelBuilder.Entity<AppTask>()
        //    //    .HasOne(t => t.CreatedByUser)
        //    //    .WithMany(u => u.CreatedUserTasks);

        //    //modelBuilder.Entity<AppTask>()
        //    //    .HasOne(t => t.AssignedToUser)
        //    //    .WithMany(u => u.AssignedUserTasks);
        //} 
        #endregion

    }

}