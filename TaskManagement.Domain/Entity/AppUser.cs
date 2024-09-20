using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entity
{
    public class AppUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<AppTask> AssignedUserTasks { get; set; }

        public ICollection<AppTask> CreatedUserTasks { get; set; }
    }
}