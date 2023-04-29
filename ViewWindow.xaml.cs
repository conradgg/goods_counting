using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace goods_counting
{
    public partial class ViewWindow : Window
    {
        DbGoods dbGoods = new DbGoods();
        List<Item> items;

        public ViewWindow()
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

        private void needName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Item> filteredList = items
                .Where(user => user.type.ToLower().Contains(needName.Text.ToLower()))
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
    }
}
