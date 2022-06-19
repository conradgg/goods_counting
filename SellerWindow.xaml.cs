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
    /// Логика взаимодействия для SellerWindow.xaml
    /// </summary>
    public partial class SellerWindow : Window
    {
        public SellerWindow()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `goods`", db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            goodsDG.ItemsSource = table.DefaultView;
        }

        private void sell_Click(object sender, RoutedEventArgs e)
        {
            if (goodsDG.SelectedItem != null)
            {
                if (count.Text != "")
                {
                    int COUNT = Convert.ToInt32(count.Text);
                    var cellInfos = goodsDG.SelectedCells;
                    var line = new List<string>();
                    foreach (DataGridCellInfo cellInfo in cellInfos)
                    {
                        if (cellInfo.IsValid)
                        {
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                            var row = (DataRowView)content.DataContext;
                            object[] obj = row.Row.ItemArray;
                            line.Add(obj[0].ToString());
                            line.Add(obj[2].ToString());
                        }
                    }
                    string id = line[0].ToString();
                    int Count = Int32.Parse(line[1]) - COUNT;
                    if (Count >= 0)
                        Update(id, Count);
                    else
                        MessageBox.Show("Недостаточно товара!");
                }
                else
                    MessageBox.Show("Введите количество!");
            }
            else
                MessageBox.Show("Товар не выбран!");
        }

        public void Update(string id, int Count)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("UPDATE `goods` SET `count` = @cT WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@cT", MySqlDbType.VarChar).Value = Count;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешно!");
            }
            else
                MessageBox.Show("Ошибка");
            db.closeConnection();
            getData();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            getData();
        }

        private void restore_Click(object sender, RoutedEventArgs e)
        {
            if (goodsDG.SelectedItem != null)
            {
                if (count.Text != "")
                {
                    int COUNT = Convert.ToInt32(count.Text);
                    var cellInfos = goodsDG.SelectedCells;
                    var line = new List<string>();
                    foreach (DataGridCellInfo cellInfo in cellInfos)
                    {
                        if (cellInfo.IsValid)
                        {
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                            var row = (DataRowView)content.DataContext;
                            object[] obj = row.Row.ItemArray;
                            line.Add(obj[0].ToString());
                            line.Add(obj[2].ToString());
                        }
                    }
                    string id = line[0].ToString();
                    int Count = Int32.Parse(line[1]) + COUNT;
                    if (Count >= 0)
                        Update(id, Count);
                    else
                        MessageBox.Show("Недостаточно товара!");
                }
                else
                    MessageBox.Show("Введите количество!");
            }
            else
                MessageBox.Show("Товар не выбран!");
        }
    }
}
