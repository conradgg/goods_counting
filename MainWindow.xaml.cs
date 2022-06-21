using MySql.Data.MySqlClient;
using System;
using System.Data;
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
            Access();
        }

        private void Access()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT users.user, users.password, roles.adminDasboardAccess, roles.sellerModeAccess, roles.viewModeAccess, roles.recordModeAcess FROM `users` INNER JOIN `roles` ON users.role = roles.role WHERE `user` = @uL AND `password` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Properties.Settings.Default.rememberUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = Properties.Settings.Default.rememberPassword;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                if (table.Rows[0][2].ToString() == "True")
                    adminDashboard.Visibility = Visibility.Visible;
                if(table.Rows[0][3].ToString() == "True")
                    sellerMode.Visibility = Visibility.Visible;
                if (table.Rows[0][4].ToString() == "True")
                    viewMode.Visibility = Visibility.Visible;
                if (table.Rows[0][5].ToString() == "True")
                    recordMode.Visibility = Visibility.Visible;
                if (table.Rows[0][2].ToString() == "False" && table.Rows[0][3].ToString() == "False" && table.Rows[0][4].ToString() == "False" && table.Rows[0][5].ToString() == "False")
                    MessageBox.Show("У вас нет доступа ко всем разделам. \nОбратитесь к администратору для выдачи доступа.");
            }
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
