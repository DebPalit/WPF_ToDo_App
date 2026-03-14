using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ToDo_App
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginLogic login = new LoginLogic(UserName.Text, PassWord.Password);
            User? user = await login.UserAuthentication();

            if (user != null)
            {
                //UserCreationLogic logic = new UserCreationLogic();
                //logic.CreateUser("test.user", "12345");

                Window.GetWindow(this).Content = new AppView(user.UserId);
            }
            else
            {
                MessageBox.Show("Username or Password invalid ", "Invalid Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
