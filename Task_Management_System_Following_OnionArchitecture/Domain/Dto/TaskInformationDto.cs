using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class TaskInformationDto
    {
        public string ProjectName { get; set; }

        public  string TeamName { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserName { get; set; }
        public string TaskAssign { get; set; }

        public DateTime Date { get; set; }
 
        public bool  IsCompleted { get; set; }
    }
}
