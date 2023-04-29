using MySqlConnector;
using System;
using System.Collections.Generic;

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
    }
}
