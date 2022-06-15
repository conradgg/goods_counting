using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            Close();
        }


        private void search_Click(object sender, RoutedEventArgs e)
        {
            string needRole = role.Text;
            string needUser = user.Text;

        DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string sql;
            if (needRole == "" && needUser == "")
                sql = "SELECT `user`, `role` FROM `users`";
            else if (needRole == "")
                sql = "SELECT `user`, `role` FROM `users` WHERE `user` = @nU";
            else if (needUser == "")
                sql = "SELECT `user`, `role` FROM `users` WHERE `role` = @nR";
            else
                sql = "SELECT `user`, `role` FROM `users` WHERE `user` = @nU AND `role` = @nR";

            MySqlCommand command = new MySqlCommand(sql, db.getConnection());
            command.Parameters.Add("@nR", MySqlDbType.VarChar).Value = needRole;
            command.Parameters.Add("@nU", MySqlDbType.VarChar).Value = needUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            usersDG.ItemsSource = table.DefaultView;
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
