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

namespace goods_counting
{
    /// <summary>
    /// Логика взаимодействия для AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
            userName.Content += Properties.Settings.Default.rememberUser;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            Users usersWindow = new Users();
            usersWindow.Show();
            Close();
        }

        private void roles_Click(object sender, RoutedEventArgs e)
        {
            Roles rolesWindow = new Roles();
            rolesWindow.Show();
            Close();
        }
    }
}
