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

            if (adminAccess())
                adminDashboard.Visibility = Visibility.Visible;
        }

        private bool adminAccess()
        {
            bool check = false;
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `user` = @uL AND `password` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Properties.Settings.Default.rememberUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = Properties.Settings.Default.rememberPassword;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                if(table.Rows[0][3].ToString() == "admin")
                    check = true;

            return check;
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
            MessageBox.Show("Good!");
        }
    }
}
