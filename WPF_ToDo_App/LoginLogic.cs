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

        public bool UserAuthentication()
        {
            if (UserName != null && PassWord != null)
            {
                //hardcoded now, will fix later
                if (UserName == "deb.palit" && PassWord == "722144")
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
