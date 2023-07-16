using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Navigation_Menu_App.Models;

namespace Navigation_Menu_App
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
           
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindowVM = new MainWindowVM();
            var adminUsername = Admin_Username.Text;
            var adminPassword = Admin_Password.Password;

            using (DatabaseContext context = new DatabaseContext())
            {

                bool adminLog = context.Admins.Any(Admin => Admin.AdminUsername == adminUsername && Admin.AdminPassword == adminPassword);

                if (adminLog)
                {
                    Admin? admin = context.Admins.FirstOrDefault(Admin => Admin.AdminUsername == adminUsername);
                    AdminWindow adminWindow = new AdminWindow(admin);
                    adminWindow.Show();
                    
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
        }
        public void AdminGrantAccess()
        {
            
        }

        public void openAdminWindow()
        {

            
            
        }

        private void smallCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
