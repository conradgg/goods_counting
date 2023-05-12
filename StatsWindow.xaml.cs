using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Paragraph = iTextSharp.text.Paragraph;

namespace goods_counting
{
    public partial class StatsWindow : Window
    {
        public StatsWindow()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            Close();
        }

        private void take_Click(object sender, RoutedEventArgs e)
        {
            DbGoods dbGoods = new DbGoods();
            List<Sell> sales;
            Document document = new Document();

            if (startDate.Text == "" || endDate.Text == "")
                snackbar.MessageQueue.Enqueue("Вы не выбрали диапазон дат!");
            else if (!DateTime.TryParse(startDate.Text, out _) || !DateTime.TryParse(endDate.Text, out _))
                snackbar.MessageQueue.Enqueue("Вы ввели некорректный диапазон дат!");
            else
            {
                sales = dbGoods.getSells(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));

                if (sales.Count == 0)
                    snackbar.MessageQueue.Enqueue("В данном диапазоне дат не было продаж!");
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Сохранить отчет о продажах";
                    saveFileDialog.FileName = "Отчет о продажах.pdf";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        document.Open();

                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;

                        BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/Arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font font = new Font(baseFont, 12, Font.NORMAL);

                        Paragraph title = new Paragraph("Отчет о продажах за период С " + startDate.Text + " ПО " + endDate.Text, font);
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);
                        document.Add(new Paragraph("\n"));
                        PdfPCell cell = new PdfPCell(new Phrase("Дата заказа", font));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Название товара", font));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Количество товаров", font));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Цена товара", font));
                        table.AddCell(cell);

                        foreach (var sale in sales)
                        {
                            cell = new PdfPCell(new Phrase(sale.time.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(sale.product.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(sale.count.ToString() + " шт.", font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(sale.price.ToString() + " р.", font));
                            table.AddCell(cell);
                        }

                        document.Add(table);
                        document.Add(new Paragraph("\n\n"));
                        Paragraph ptp = new Paragraph("Отчет подготовлен пользователем " + Properties.Settings.Default.rememberUser, font);
                        document.Add(ptp);
                        document.Close();
                    }
                }
            }            
        }
    }
}
