using System.Windows;

namespace goods_counting
{
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

        private void stats_Click(object sender, RoutedEventArgs e)
        {
            StatsWindow statsWindow = new StatsWindow();
            statsWindow.Show();
            Close();
        }
    }
}
