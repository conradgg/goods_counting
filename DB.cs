using MySqlConnector;

namespace goods_counting
{
    public class DB
    {
        public MySqlConnection getConnection()
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "goodscounting",
                UserID = "test",
                Password = "jIXZTxVkYldUp7makqeMo9Vktmntcvsu",
            };
            string connectionString = connectionStringBuilder.ConnectionString;
            return new MySqlConnection(connectionString);
        }
    }
}
