using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TaskManagement.BusinessLayer.Services;
using TaskManagement.DataLayer;
using TaskManagement.DataLayer.Repository;
using TaskManagement.Domain.IRepository;
using System;
using TaskManagement.Domain.Entity;
using TaskManagement.DataLayer.Configurations;


namespace TaskManagementApp
{
    class MainApp
    {

        public static void Main(string[] args)
        {

            #region MyRegion
            //// var x=WebApplication
            ////DI kullanımı, UserService - TaskService için
            var serviceProvider = new ServiceCollection()
            .AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-N6IKK70;Initial Catalog=TaskAppDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
            .AddScoped<IAppTaskRepository, AppTaskRepository>()
            .AddScoped<IAppUserRepository, AppUserRepository>()
            //.AddTransient<TaskService>()
            .BuildServiceProvider();


            //var serviceProvider = new ServiceCollection();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskManagementDbContext>();
                var taskService = scope.ServiceProvider.GetRequiredService<IAppTaskRepository>();
                var userService = scope.ServiceProvider.GetRequiredService<IAppUserRepository>();


                var list = context.AppUser.ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.UserName);
                }


                var loggedInUser = StartUserOperations(userService);

                if (loggedInUser != null)
                {
                    ManageTasks(taskService, loggedInUser);
                }
                else
                {
                    Console.WriteLine("Geçersiz kullanıcı giriş denemesi.");
                }
            }
            #endregion





        }



        private static List<AppUser> ListUser(AppUserRepository appUserRepository)
        {
            var list = appUserRepository.GetAll();

            foreach (var item in list)
            {
                Console.WriteLine(item.UserName);
                Console.WriteLine(item.Password);
            }
            return list;
        }
        private static AppUser StartUserOperations(AppUserRepository userService)
        {
            Console.WriteLine("1. Kullanıcı Kayıt");
            Console.WriteLine("2. Kullanıcı Giriş");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                RegisterUser(userService);
            }
            else if (choice == "2")
            {
                return userService.Login();
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
            return null;
        }
        private static void RegisterUser(AppUserRepository userService)
        {
            Console.Write("Kullanıcı adınızı girin: ");
            var username = Console.ReadLine();

            Console.Write("Şifrenizi girin: ");
            var password = Console.ReadLine();
            //null olmasını önlemek lazım
            userService.Register(username, password);

        }

        private static void ManageTasks(AppTaskRepository taskService, AppUser loggedInUser)
        {
            while (true)
            {
                Console.WriteLine("\nSeçenekler:");
                Console.WriteLine("1 - Üzerimdeki Taskları Görüntüle");
                Console.WriteLine("2 - Yeni Task Yarat");
                Console.WriteLine("3 - Task Güncelle");
                Console.WriteLine("4 - Task Sil");
                Console.WriteLine("5 - Çıkış");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAssignedTasks(taskService, loggedInUser.Id);
                        break;
                    case "2":
                        CreateNewTask(taskService, loggedInUser.Id);
                        break;
                    case "3":
                        UpdateTask(taskService, loggedInUser.Id);
                        break;
                    case "4":
                        DeleteTask(taskService, loggedInUser.Id);
                        break;
                    case "5":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }

        private static void DisplayAssignedTasks(AppTaskRepository taskService, int userId)
        {
            var assignedTasks = taskService.GetAssignedTasks(userId);
            Console.WriteLine("\nÜzerimdeki Tasklar:");

            foreach (var task in assignedTasks)
            {
                Console.WriteLine($"Task Id: {task.Id}, Konu: {task.Title}, Statü: {task.TaskStatus}");
            }
        }

        private static void CreateNewTask(AppTaskRepository taskService, int createdByUserId)
        {
            AppTask newTask = new AppTask();
            Console.Write("Task başlığı girin: ");
            newTask.Title = Console.ReadLine();

            Console.Write("Task açıklaması girin: ");
            newTask.Description = Console.ReadLine();

            Console.Write("Atanacak kullanıcı Id'sini girin: ");
            newTask.AssignedToUserId = int.Parse(Console.ReadLine());

            taskService.CreateTask(newTask);
            Console.WriteLine("Yeni task oluşturuldu.");
        }

        private static void UpdateTask(AppTaskRepository taskService, int createdByUserId)
        {

            AppTask newTask = new AppTask();
            Console.Write("Güncellemek istediğiniz task Id'sini girin: ");
            newTask.Id = int.Parse(Console.ReadLine());

            Console.Write("Yeni task başlığını girin: ");
            newTask.Title = Console.ReadLine();

            Console.Write("Yeni task açıklamasını girin: ");
            newTask.Description = Console.ReadLine();
            newTask.CreatedByUserId = createdByUserId;

            taskService.UpdateTask(newTask);
            Console.WriteLine("Task güncellendi.");
        }

        private static void DeleteTask(AppTaskRepository taskService, int createdByUserId)
        {
            Console.Write("Silmek istediğiniz task Id'sini girin: ");
            var taskId = int.Parse(Console.ReadLine());

            taskService.DeleteTask(taskId, createdByUserId);
            Console.WriteLine("Task silindi (henüz başlamamış).");
        }

    }
}

