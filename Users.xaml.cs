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
                sql = "SELECT `id`, `user`, `role` FROM `users`";
            else if (needRole == "")
                sql = "SELECT `id`, `user`, `role` FROM `users` WHERE `user` = @nU";
            else if (needUser == "")
                sql = "SELECT `id`, `user`, `role` FROM `users` WHERE `role` = @nR";
            else
                sql = "SELECT `id`, `user`, `role` FROM `users` WHERE `user` = @nU AND `role` = @nR";

            MySqlCommand command = new MySqlCommand(sql, db.getConnection());
            command.Parameters.Add("@nR", MySqlDbType.VarChar).Value = needRole;
            command.Parameters.Add("@nU", MySqlDbType.VarChar).Value = needUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            usersDG.ItemsSource = table.DefaultView;
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            var cellInfos = usersDG.SelectedCells;
            var line = new List<string>();
            foreach (DataGridCellInfo cellInfo in cellInfos)
            {
                if (cellInfo.IsValid)
                {
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                    var row = (DataRowView)content.DataContext;
                    object[] obj = row.Row.ItemArray;
                    line.Add(obj[0].ToString());
                    line.Add(obj[1].ToString());
                    line.Add(obj[2].ToString());
                }
            }
            string selectedid = line[0].ToString();
            string newUserName = line[1].ToString();
            string newRole = line[2].ToString();
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("UPDATE `users` SET `user` = @nU, `role` = @nR WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = selectedid;
            command.Parameters.Add("@nU", MySqlDbType.VarChar).Value = newUserName;
            command.Parameters.Add("@nR", MySqlDbType.VarChar).Value = newRole;
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Запись изменена!");
            }
            else
                MessageBox.Show("Ошибка");
            db.closeConnection();
            search_Click(sender, e);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var cellInfos = usersDG.SelectedCells;
            var line = new List<string>();
            foreach (DataGridCellInfo cellInfo in cellInfos)
            {
                if (cellInfo.IsValid)
                {
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                    var row = (DataRowView)content.DataContext;
                    object[] obj = row.Row.ItemArray;
                    line.Add(obj[0].ToString());
                }
            }
            string selectedid = line[0].ToString();
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("DELETE  FROM `users` WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = selectedid;
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Запись удалена!");
            }
            else
                MessageBox.Show("Ошибка");
            db.closeConnection();
            search_Click(sender, e);
        }
    }
}
