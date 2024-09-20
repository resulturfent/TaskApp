using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Domain.IRepository
{
    public interface IAppTaskRepository:IGenericRepository<AppTask>
    {
        //varsa farklı işlemerii gerçekleştirecek method yazılır
        List<AppTask> GetAssignedTasks(int userId);
        void CreateTask(AppTask appTask);
        void UpdateTask(AppTask appTask);
        void DeleteTask(int taskId, int userId);
        //void AssignTaskToUser(int taskId, int userId);



    }
}
