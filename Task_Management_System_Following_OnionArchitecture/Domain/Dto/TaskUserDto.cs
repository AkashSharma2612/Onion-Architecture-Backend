using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class TaskUserDto
    {
        public List<int> TaskIds { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate  { get; set; }
        public bool IsCompleted { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }


    }
}
