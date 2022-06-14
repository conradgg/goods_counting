using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace goods_counting
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            user.Focus();

            if (Properties.Settings.Default.rememberAuth)
                getRememberLogin();
        }

        private void authorize_Click(object sender, RoutedEventArgs e)
        {
            Encryption encryption = new Encryption();

            String login = user.Text;
            String password = passwd.Password;

            if (login == "" || password == "")
                MessageBox.Show("Необходимо заполнить все поля");
            else
            {
                if (Authorize(login, encryption.Encrypt(password)))
                {
                    Properties.Settings.Default.rememberUser = login;

                    if (rememberPasswd.IsChecked == true)
                    {
                        Properties.Settings.Default.rememberAuth = true;
                        Properties.Settings.Default.rememberPassword = password;
                    }

                    Properties.Settings.Default.Save();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Введен неверный логин или пароль");
            }
        }

        private bool Authorize(string login, string password)
        {
            bool check =false;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `user` = @uL AND `password` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                check = true;

            return check;
        }

        private void getRememberLogin()
        {
            String login = Properties.Settings.Default.rememberUser;
            String password = Properties.Settings.Default.rememberPassword;

            if (Authorize(login, password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                Properties.Settings.Default.rememberAuth = false;
                Properties.Settings.Default.rememberUser = null;
                Properties.Settings.Default.rememberPassword = null;
                Properties.Settings.Default.Save();

                MessageBox.Show("Данные авторизации устарели");
            }
        }

        private void register_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }
    }
}
