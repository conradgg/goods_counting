using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace goods_counting
{
    public partial class RecordWindow : Window
    {
        DbGoods dbGoods = new DbGoods();
        List<Item> items;

        public RecordWindow()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            needName.Text = string.Empty;
            items = dbGoods.getGoods();
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
            else if (!Int32.TryParse(count.Text, out _))
                snackbar.MessageQueue.Enqueue("Количество товара введено некорректно!");
            else if (Int32.Parse(count.Text) < 0)
                snackbar.MessageQueue.Enqueue("Количество товара не может быть отрицательным!");
            else if (price.Text == "")
                snackbar.MessageQueue.Enqueue("Введите цену!");
            else if (!float.TryParse(price.Text, out _))
                snackbar.MessageQueue.Enqueue("Цена введена некорректно!");
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
                if (!items.Any(product => product.type.ToLower().Equals(newItem.type.ToLower())))
                {
                    dbGoods.addGoods(newItem);
                    snackbar.MessageQueue.Enqueue("Товар добавлен!");
                    name.Text = string.Empty;
                    count.Text = string.Empty;
                    price.Text = string.Empty;
                    getData();
                }
                else
                    snackbar.MessageQueue.Enqueue("Такой товар уже есть!");
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
                getData();
                snackbar.MessageQueue.Enqueue("Товар обновлен!");
            }
        }

        private void needName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Item> filteredList = items
                .Where(product => product.type.ToLower().Contains(needName.Text.ToLower()))
                .ToList();
            goodsDG.ItemsSource = filteredList;
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

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            Item selectedRow = goodsDG.SelectedItem as Item;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали товар!");
            else
            {
                dbGoods.removeProduct(selectedRow.id);
                getData();
                snackbar.MessageQueue.Enqueue("Товар удален!");
            }
        }
    }
}
