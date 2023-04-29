using System;
using System.Collections.Generic;
using System.Windows;

namespace goods_counting
{
    public partial class RecordWindow : Window
    {
        DbGoods dbGoods = new DbGoods();

        public RecordWindow()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            List<Item> items = dbGoods.getGoods();
            goodsDG.DataContext = items;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
                snackbar.MessageQueue.Enqueue("Введите название товара!");
            else if (count.Text == "")
                snackbar.MessageQueue.Enqueue("Введите количество товара!");
            else if (count.Text.Contains(".") && count.Text.Contains(","))
                snackbar.MessageQueue.Enqueue("Количество товара может быть только целым числом!");
            else if (Int32.Parse(count.Text) < 0)
                snackbar.MessageQueue.Enqueue("Количество товара не может быть отрицательным!");
            else if (price.Text == "")
                snackbar.MessageQueue.Enqueue("Введите цену!");
            else if (float.Parse(price.Text) < 0)
                snackbar.MessageQueue.Enqueue("Цена не может быть отрицательной!");
            else
            {
                var newItem = new Item
                {
                    type = name.Text.ToString(),
                    count = Convert.ToInt32(count.Text),
                    price = decimal.Parse(price.Text)
                };
                dbGoods.addGoods(newItem);
                snackbar.MessageQueue.Enqueue("Товар добавлен!");
                getData();
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Item selectedRow = goodsDG.SelectedItem as Item;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали товар!");
            else
            {
                dbGoods.updateGoods(selectedRow);
                snackbar.MessageQueue.Enqueue("Товар обновлен!");
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
