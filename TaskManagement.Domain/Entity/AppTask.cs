using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.Entity
{
    public class AppTask:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int  TaskStatus { get; set; }
        public int CreatedByUserId { get; set; }      
        public int AssignedToUserId { get; set; }

        public AppUser AssignedToUser { get; set; }
        public AppUser CreatedByUser { get; set; }

    }
}