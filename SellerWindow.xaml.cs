using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace goods_counting
{
    public partial class SellerWindow : Window
    {
        DbGoods dbGoods = new DbGoods();
        List<Item> items;

        public SellerWindow()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            needName.Text = string.Empty;
            items = dbGoods.getGoods();
            goodsDG.ItemsSource = items;
        }

        private void sell_Click(object sender, RoutedEventArgs e)
        {
            Item selectedRow = goodsDG.SelectedItem as Item;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали товар!");
            else if (count.Text.Length == 0)
                snackbar.MessageQueue.Enqueue("Вы не ввели количество!");
            else if (selectedRow.count - Convert.ToInt32(count.Text) < 0)
                snackbar.MessageQueue.Enqueue("Вы пытаетесь продать больше, чем есть в наличии!");
            else
            {
                selectedRow.count -= Convert.ToInt32(count.Text);
                goodsDG.Items.Refresh();
                dbGoods.updateGoods(selectedRow);
                snackbar.MessageQueue.Enqueue("Вы продали товар!");
            }
        }

        private void needName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Item> filteredList = items
                .Where(user => user.type.ToLower().Contains(needName.Text.ToLower()))
                .ToList();
            goodsDG.ItemsSource = filteredList;
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

        private void refund_Click(object sender, RoutedEventArgs e)
        {
            Item selectedRow = goodsDG.SelectedItem as Item;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали товар!");
            else if (count.Text.Length == 0)
                snackbar.MessageQueue.Enqueue("Вы не ввели количество!");
            else
            {
                selectedRow.count += Convert.ToInt32(count.Text);
                goodsDG.Items.Refresh();
                dbGoods.updateGoods(selectedRow);
                snackbar.MessageQueue.Enqueue("Вы вернули товар!");
            }
        }

        private void count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int _))
            {
                e.Handled = true;
            }
        }

        private void count_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox count = (TextBox)sender;

            if (!string.IsNullOrEmpty(count.Text) && int.TryParse(count.Text, out int value))
            {
                if (value < 1 || value > 1000)
                {
                    count.Text = "";
                }
            }
        }
    }
}
