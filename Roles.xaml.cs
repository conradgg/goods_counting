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
    /// Логика взаимодействия для Roles.xaml
    /// </summary>
    public partial class Roles : Window
    {
        public Roles()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `roles`", db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            rolesDG.ItemsSource = table.DefaultView;
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            var cellInfos = rolesDG.SelectedCells;
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
                    line.Add(obj[3].ToString());
                    line.Add(obj[4].ToString());
                }
            }
            string selectedRole = line[0].ToString();
            string ada = line[1].ToString();
            string sma = line[2].ToString();
            string vma = line[3].ToString();
            string rma = line[4].ToString();

            int ADA = 0, SMA = 0, VMA = 0, RMA = 0;

            if(ada == "True")
                ADA = 1;
            if(sma == "True")
                SMA = 1;
            if (vma == "True")
                VMA = 1;
            if (rma == "True")
                RMA = 1;

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("UPDATE `roles` SET `adminDasboardAccess` = @ADA, `sellerModeAccess` = @SMA, `viewModeAccess` = @VMA, `recordModeAcess` = @RMA WHERE `role` = @sR", db.getConnection());
            command.Parameters.Add("@sR", MySqlDbType.VarChar).Value = selectedRole;
            command.Parameters.Add("@ADA", MySqlDbType.VarChar).Value = ADA;
            command.Parameters.Add("@SMA", MySqlDbType.VarChar).Value = SMA;
            command.Parameters.Add("@VMA", MySqlDbType.VarChar).Value = VMA;
            command.Parameters.Add("@RMA", MySqlDbType.VarChar).Value = RMA;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Роль изменена!");
            }
            else
                MessageBox.Show("Ошибка");
            db.closeConnection();
            getData();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var cellInfos = rolesDG.SelectedCells;
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

            MySqlCommand command = new MySqlCommand("DELETE  FROM `roles` WHERE `role` = @R", db.getConnection());
            command.Parameters.Add("@R", MySqlDbType.VarChar).Value = selectedid;
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Роль удалена!");
            }
            else
                MessageBox.Show("Ошибка");
            db.closeConnection();
            getData();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (newRole.Text == "")
                MessageBox.Show("Введите название роли!");
            else
            {
                int ADA = 0, SMA = 0, VMA = 0, RMA = 0;

                if (adminAccess.IsChecked == true)
                    ADA = 1;
                if (sellerAccess.IsChecked == true)
                    SMA = 1;
                if (viewAccess.IsChecked == true)
                    VMA = 1;
                if (addAccess.IsChecked == true)
                    RMA = 1;

                DB db = new DB();

                MySqlCommand command = new MySqlCommand("insert into `roles` values(@nR, @ADA, @SMA, @VMA, @RMA);", db.getConnection());
                command.Parameters.Add("@nR", MySqlDbType.VarChar).Value = newRole.Text;
                command.Parameters.Add("@ADA", MySqlDbType.VarChar).Value = ADA;
                command.Parameters.Add("@SMA", MySqlDbType.VarChar).Value = SMA;
                command.Parameters.Add("@VMA", MySqlDbType.VarChar).Value = VMA;
                command.Parameters.Add("@RMA", MySqlDbType.VarChar).Value = RMA;

                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Роль добавлена!");
                }
                else
                    MessageBox.Show("Ошибка");
                db.closeConnection();
                getData();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            Close();
        }
    }
}
