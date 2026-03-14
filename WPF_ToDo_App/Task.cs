using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_ToDo_App
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string? TaskDetails { get; set; }

        public Guid UserID { get; set; }
        public User? User { get; set; }
    }
}
