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
    /// Логика взаимодействия для RecordWindow.xaml
    /// </summary>
    public partial class RecordWindow : Window
    {
        public RecordWindow()
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

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
                MessageBox.Show("Введите название товара!");
            else if (count.Text == "")
                MessageBox.Show("Введите количество товара!");
            else if (count.Text.Contains(".") && count.Text.Contains(","))
                MessageBox.Show("Количество товара может быть только целым числом!");
            else if (Int32.Parse(count.Text) < 0)
                MessageBox.Show("Количество товара не может быть отрицательным!");
            else if (price.Text == "")
                MessageBox.Show("Введите цену!");
            else if (price.Text.Contains("."))
                MessageBox.Show("Необходимо использовать запятую, вместо точки!");
            else if (float.Parse(price.Text) < 0)
                MessageBox.Show("Цена не может быть отрицательной!");
            else
            {
                DB db = new DB();

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("insert into `goods` values (null, @nM, @cT, @pE)", db.getConnection());
                command.Parameters.Add("@nM", MySqlDbType.VarChar).Value = name.Text.ToString();
                command.Parameters.Add("@cT", MySqlDbType.VarChar).Value = Int32.Parse(count.Text);
                command.Parameters.Add("@pE", MySqlDbType.VarChar).Value = float.Parse(price.Text).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"));

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                    MessageBox.Show("Ошибка!");
                else
                {
                    MessageBox.Show("Успешно!");
                    name.Text = "";
                    count.Text = "";
                    price.Text = "";
                }
                getData();
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
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
                    line.Add(obj[1].ToString());
                    line.Add(obj[2].ToString());
                    line.Add(obj[3].ToString());
                }
            }
            string selectedid = line[0].ToString();
            string type = line[1].ToString();
            int count = Int32.Parse(line[2]);
            if (line[3].Contains("."))
                MessageBox.Show("Необходимо использовать запятую, вместо точки!");
            else
            {
                var Price = float.Parse(line[3]).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                DB db = new DB();

                MySqlCommand command = new MySqlCommand("UPDATE `goods` SET `type` = @tP, `count` = @cT, `price` = @pE WHERE `id` = @id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = selectedid;
                command.Parameters.Add("@tP", MySqlDbType.VarChar).Value = type;
                command.Parameters.Add("@cT", MySqlDbType.VarChar).Value = count;
                command.Parameters.Add("@pE", MySqlDbType.VarChar).Value = Price;
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Товар изменен!");
                }
                else
                    MessageBox.Show("Ошибка");
                db.closeConnection();
                getData();
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            getData();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
