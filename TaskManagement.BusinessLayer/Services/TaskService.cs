using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.DataLayer;
using TaskManagement.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Enum;

namespace TaskManagement.BusinessLayer.Services
{

    public class TaskService
    {
        private readonly TaskManagementDbContext taskDbContext;

        public TaskService(TaskManagementDbContext context)
        {
            taskDbContext = context;
        }
        public List<AppTask> GetAssignedTasks(int userId)
        {
            return taskDbContext.AppTask.Where(t => t.AssignedToUserId == userId).ToList();
        }

        public void CreateTask(string title, string description, int createdByUserId, int assignedToUserId)
        {
            var newTask = new AppTask
            {
                Title = title,
                Description = description,
                TaskStatus = AppTaskStatus.NotStarted.GetHashCode(),
                CreatedByUserId = createdByUserId,
                AssignedToUserId = assignedToUserId
            };

            taskDbContext.AppTask.Add(newTask);
            taskDbContext.SaveChanges();
            Console.WriteLine("Task başarıyla oluşturuldu.");
        }

        public void UpdateTask(int taskId, string title, string description, int userId)
        {
            var task = taskDbContext.AppTask.FirstOrDefault(t => t.Id == taskId && t.CreatedByUserId == userId);

            if (task != null)
            {
                task.Title = title;
                task.Description = description;
                //task.TaskStatus = askStatus;
                taskDbContext.SaveChanges();
                Console.WriteLine("Task başarıyla güncellendi.");
            }
            else
            {
                Console.WriteLine("Task mevcut değil.");
            }
        }

        public void DeleteTask(int taskId, int userId)
        {
            var task = taskDbContext.AppTask.FirstOrDefault(t => t.Id == taskId && t.CreatedByUserId == userId);

            if (task != null)
            {
                if (task.TaskStatus == AppTaskStatus.NotStarted.GetHashCode())
                {
                    taskDbContext.AppTask.Remove(task);
                    taskDbContext.SaveChanges();
                    Console.WriteLine("Task başarıyla silindi.");
                }
                else
                {
                    Console.WriteLine("Task başlamış olduğu için silinemez.");
                }
            }
            else
            {
                Console.WriteLine("Task bulunamadı.");
            }
        }


        public void AssignTaskToUser(int taskId, int userId)
        {
            var task = taskDbContext.AppTask.FirstOrDefault(t => t.Id == taskId);

            if (task != null)
            {
                task.AssignedToUserId = userId;
                taskDbContext.SaveChanges();
                Console.WriteLine("Task başarıyla kullanıcıya atandı.");
            }
            else
            {
                Console.WriteLine("Task bulunamadı.");
            }
        }

    }

}
