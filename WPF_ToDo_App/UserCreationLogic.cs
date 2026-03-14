using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_ToDo_App
{
    public class UserCreationLogic
    {
        AppDbContext db = new AppDbContext();
         public void CreateUser(string username, string password)
        {
            try 
            {
                User newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                db.UserDetails.Add(newUser);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or show a message to the user
                Console.WriteLine($"Error creating user: {ex.Message}");
            }

        }
    }
}
