using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.IRepository;

namespace TaskManagement.DataLayer.Repository
{
    public class AppTaskRepository : GenericRepository<AppTask>, IAppTaskRepository
    {
        public AppTaskRepository(TaskManagementDbContext taskManagementDbContext) : base(taskManagementDbContext)
        {

        }

        public List<AppTask> GetAssignedTasks(int userId)
        {
            return _taskManagementDb.AppTask.Where(t => t.AssignedToUserId == userId).ToList();
        }

        public void CreateTask(AppTask appTask)
        {
            try
            {
                var existsData = Any(k => k.Title == appTask.Title);
                //var x = _taskManagementDb.AppTask.Any(k => k.Title == appTask.Title);

                if (existsData)
                {
                    Console.WriteLine("Böyle bir data mevcut");
                    return;//işlemii sonlandırır ama method void
                    //else yazmamak için return kullandım
                }

                var newTask = new AppTask
                {
                    Title = appTask.Title,
                    Description = appTask.Description,
                    TaskStatus = appTask.TaskStatus,
                    CreatedByUserId = appTask.CreatedByUserId,
                    AssignedToUserId = appTask.AssignedToUserId
                };

                //_taskManagementDb.AppTask.Add(newTask);
                //_taskManagementDb.SaveChanges();
                Add(newTask);

                Console.WriteLine("Task başarıyla oluşturuldu.");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu:" + ex.Message);

            }
        }

        public void UpdateTask(AppTask appTask)
        {
            try
            {
                //var task = _taskManagementDb.AppTask.FirstOrDefault(t => t.Id == appTask.Id && t.CreatedByUserId == appTask.CreatedByUserId);
                var task = GetQuery(k => k.Id == appTask.Id && k.CreatedByUserId == appTask.CreatedByUserId);

                if (task != null)
                {
                    task.Title = appTask.Title;
                    task.Description = appTask.Description;
                    task.TaskStatus = appTask.TaskStatus;
                    task.CreatedByUserId = appTask.CreatedByUserId;
                    task.AssignedToUserId = appTask.AssignedToUserId;

                    Update(task);
                    Console.WriteLine("Task başarıyla güncellendi.");
                }
                else
                {
                    Console.WriteLine("Task mevcut değil.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu:" + ex.Message);
            }
        }

        public void DeleteTask(int taskId, int userId)
        {
            //var task = _taskManagementDb.AppTask.FirstOrDefault(t => t.Id == taskId && t.CreatedByUserId == userId);
            var task = GetQuery(t => t.Id == taskId && t.CreatedByUserId == userId);

            if (task != null)
            {
                if (task.TaskStatus == AppTaskStatus.NotStarted.GetHashCode())
                {
                    //    _taskManagementDb.AppTask.Remove(task);
                    //    _taskManagementDb.SaveChanges();
                    Remove(task);
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


        //public void AssignTaskToUser(int taskId, int userId)
        //{
        //    //var task = _taskManagementDb.AppTask.FirstOrDefault(t => t.Id == taskId);
        //    var task = GetQuery(t => t.Id == taskId);

        //    if (task != null)
        //    {
        //        task.AssignedToUserId = userId;
        //        _taskManagementDb.SaveChanges();
        //        Console.WriteLine("Task başarıyla kullanıcıya atandı.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Task bulunamadı.");
        //    }
        //}
    }
}
