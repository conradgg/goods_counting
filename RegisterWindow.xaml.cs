using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace goods_counting
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            user.Focus();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            String login = user.Text;
            String password = passwd.Password;

            if (login == "" || password == "")
                MessageBox.Show("Необходимо заполнить все поля");
            else if (login.Length < 4)
                MessageBox.Show("Имя пользователя должно быть не менее 4 символов");
            else if (!userCheck(login))
                MessageBox.Show("Такое имя пользователя уже существует");
            else if (password != passwd_2.Password)
                MessageBox.Show("Пароли не совпадают");
            else if (password.Length < 8)
                MessageBox.Show("Длинна пароля должна быть не менее 8 символов");
            else
                Register(login, password);
        }

        private void Register(string login, string password)
        {
            Encryption encryption = new Encryption();

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (user, password, role) values (@uL, @uP, 'user')", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = encryption.Encrypt(password);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Регистация прошла успешно!");
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Close();
            }
            else
                MessageBox.Show("Ошибка");

            db.closeConnection();
        }

        private bool userCheck(string login)
        {
            bool check = true;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `user` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                check = false;

            return check;
        }

        private void authozire_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
