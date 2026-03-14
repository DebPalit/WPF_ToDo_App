using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WPF_ToDo_App
{
    public class TaskCRUDLogic
    {
        public async Task<Guid> CreateTaskAsync(Guid userid, DateTime datetime, string details)
        {
            AppDbContext db = new AppDbContext();
            Task newTask = new Task
            {
                TaskId = new Guid(),
                CreationDateTime = datetime,
                TaskDetails = details,
                UserID = userid
            };

            db.TaskDetails.Add(newTask);
            await db.SaveChangesAsync();
            return newTask.TaskId;
        }

        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            AppDbContext db = new AppDbContext();
            Task? taskToDelete = await db.TaskDetails.FindAsync(taskId);
            if (taskToDelete != null)
            {
                db.TaskDetails.Remove(taskToDelete);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Task>> GetTasksByUserIdAsync(Guid userId)
        {
            AppDbContext db = new AppDbContext();
            return await db.TaskDetails.Where(t => t.UserID == userId).ToListAsync();
        }
    }
}
