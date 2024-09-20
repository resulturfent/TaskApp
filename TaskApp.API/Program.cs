
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagement.BusinessLayer;
using TaskManagement.DataLayer;
using TaskManagement.Domain;

namespace TaskApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<TaskManagementDbContext>(
                k => k.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), d =>
                {
                    d.MigrationsAssembly(Assembly.GetAssembly(typeof(TaskManagementDbContext)).GetName().Name);//??

                })                
                
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
