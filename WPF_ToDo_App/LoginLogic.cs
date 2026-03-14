using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_ToDo_App
{
    internal class LoginLogic
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public LoginLogic(string username, string password)
        {
            UserName = username;
            PassWord = password;
        }

        public async Task<User?> UserAuthentication()
        {
            if (UserName != null && PassWord != null)
            {
                AppDbContext db = new AppDbContext();
                User? user = await db.UserDetails.FirstOrDefaultAsync(u => u.Username == UserName);

                if (user == null)
                    return null;

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(PassWord, user.PasswordHash);

                return isPasswordValid ? user : null;
            }

            else
                return null;
        }
    }
}
