using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_ToDo_App
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
