using System.Windows;

namespace goods_counting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            userName.Content += Properties.Settings.Default.rememberUser;
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

        }
    }
}
