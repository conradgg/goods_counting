using MySqlConnector;
using System;
using System.Collections.Generic;
using static iTextSharp.text.pdf.AcroFields;

namespace goods_counting
{
    internal class DbGoods : DB
    {
        public List<Item> getGoods()
        {
            List<Item> products = new List<Item>();

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM goods";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Item
                            {
                                id = Convert.ToInt32(reader["id"]),
                                type = Convert.ToString(reader["type"]),
                                count = Convert.ToInt32(reader["count"]),
                                price = decimal.Parse(Convert.ToString(reader["price"]))
                            };
                            products.Add(product);
                        }
                    }
                }

                connection.Close();
            }

            return products;
        }

        public void updateGoods(Item item)
        {
            var connection = getConnection();

            string query = "UPDATE goods SET type = @type, count = @count, price = @price WHERE id = @id";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", item.type);
                    command.Parameters.AddWithValue("@count", item.count);
                    command.Parameters.AddWithValue("@price", item.price);
                    command.Parameters.AddWithValue("@id", item.id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void addGoods(Item item)
        {
            var connection = getConnection();

            string query = "INSERT INTO goods (type, count, price) VALUES (@type, @count, @price)";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", item.type);
                    command.Parameters.AddWithValue("@count", item.count);
                    command.Parameters.AddWithValue("@price", item.price);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void removeProduct(int id)
        {
            var connection = getConnection();

            string query = "DELETE FROM goods WHERE id = @id";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void addSell(Sell item)
        {
            var connection = getConnection();

            string query = "INSERT INTO sells (product, count, price) VALUES (@product, @count, @price)";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product", item.product);
                    command.Parameters.AddWithValue("@count", item.count);
                    command.Parameters.AddWithValue("@price", item.price);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Sell> getSells(DateTime start, DateTime end)
        {
            List<Sell> sells = new List<Sell>();

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM sells WHERE time BETWEEN @start AND @end";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", start);
                    command.Parameters.AddWithValue("@end", end);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sell = new Sell
                            {
                                id = Convert.ToInt32(reader["id"]),
                                time = Convert.ToDateTime(reader["time"]),
                                product = Convert.ToString(reader["product"]),
                                count = Convert.ToInt32(reader["count"]),
                                price = decimal.Parse(Convert.ToString(reader["price"]))
                            };
                            sells.Add(sell);
                        }
                    }
                }
                connection.Close();
            }
            return sells;
        }
    }
}
