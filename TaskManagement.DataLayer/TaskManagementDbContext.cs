using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DataLayer.Configurations;
using TaskManagement.Domain.Entity;

namespace TaskManagement.DataLayer
{
    public class TaskManagementDbContext : DbContext
    {
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<AppTask> AppTask { get; set; }


        //public TaskManagementDbContext()
        //{
                
        //}

        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //AppUserConfiguration.Configure(modelBuilder);
            //AppTaskConfiguration.Configure(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//her bir tablonun Configuration  clasalarını ayrı ayrı bağlamak yerine bu şekilde Assembly ile kalıtım alan bütün classlara uygulatarak işlemiş tek bir satırta yaptırdım

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
           
        //    // Connection
        //    optionsBuilder.UseSqlServer("Server=DENIZMONSTER\\SQLEXPRESS ; Database=TaskManagementApp; Trusted_Connection=True;TrustServerCertificate=True;");
        //}
    }
}

