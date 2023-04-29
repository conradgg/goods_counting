using System.Windows;

namespace goods_counting
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            userName.Content += Properties.Settings.Default.rememberUser;
            Access();
        }

        private void Access()
        {
            DbUsers dbUsers = new DbUsers();
            var roles = dbUsers.getRoleInfo(dbUsers.getUserRole(Properties.Settings.Default.rememberUser));
            Role userRoles = roles[0];
            if (userRoles.adminDasboardAccess)
                adminDashboard.Visibility = Visibility.Visible;
            if (userRoles.sellerModeAccess)
                sellerMode.Visibility = Visibility.Visible;
            if (userRoles.viewModeAccess)
                viewMode.Visibility = Visibility.Visible;
            if (userRoles.recordModeAcess)
                recordMode.Visibility = Visibility.Visible;
            if (!userRoles.adminDasboardAccess && !userRoles.sellerModeAccess && !userRoles.viewModeAccess && !userRoles.recordModeAcess)
                snackbar.MessageQueue.Enqueue("У вас нет доступа ко всем разделам. \nОбратитесь к администратору для выдачи доступа.");
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.rememberAuth = false;
            Properties.Settings.Default.rememberUser = null;
            Properties.Settings.Default.rememberPassword = null;
            Properties.Settings.Default.Save();
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        private void adminDashboard_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDash = new AdminDashboard();
            adminDash.Show();
            Close();
        }

        private void recordMode_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow recordWindow = new RecordWindow();
            recordWindow.Show();
            Close();
        }

        private void viewMode_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
            Close();
        }

        private void sellerMode_Click(object sender, RoutedEventArgs e)
        {
            SellerWindow sellerWindow = new SellerWindow();
            sellerWindow.Show();
            Close();
        }
    }
}
