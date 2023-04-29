using MySqlConnector;
using System;
using System.Collections.Generic;

namespace goods_counting
{
    public class DbUsers : DB
    {
        public bool userExist(string username)
        {
            var res = false;
            var connection = getConnection();
            connection.Open();
            string query = "SELECT * FROM users WHERE user = @username";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    res = true;
                }
            }
            connection.Close();
            return res;
        }

        public void userRegister(User user)
        {
            var connection = getConnection();
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO users (user, password, role) VALUES (@username, @password, @role)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.user);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.Parameters.AddWithValue("@role", user.role);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public bool userAuth(User user)
        {
            var connection = getConnection();
            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM users WHERE user = @username AND password = @password";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.user);
                    command.Parameters.AddWithValue("@password", user.password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }

                connection.Close();
            }
            return false;
        }

        public string getUserRole(string user)
        {
            string role = null;

            var connection = getConnection();

            using (connection)
            {
                connection.Open();
                string query = "SELECT role FROM users WHERE user = @username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        role = Convert.ToString(reader["role"]);
                    }
                }
            }
            return role;
        }

        public List<Role> getRoleInfo(string role)
        {
            List<Role> roles = new List<Role>();

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM roles WHERE role = @role";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@role", role);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Role
                            {
                                role = Convert.ToString(reader["role"]),
                                adminDasboardAccess = Convert.ToBoolean(reader["adminDasboardAccess"]),
                                sellerModeAccess = Convert.ToBoolean(reader["sellerModeAccess"]),
                                viewModeAccess = Convert.ToBoolean(reader["viewModeAccess"]),
                                recordModeAcess = Convert.ToBoolean(reader["recordModeAcess"]),
                            };
                            roles.Add(product);
                        }
                    }
                }

                connection.Close();
            }

            return roles;
        }

        public List<Role> getRoles()
        {
            List<Role> roles = new List<Role>();

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM roles";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Role
                            {
                                role = Convert.ToString(reader["role"]),
                                adminDasboardAccess = Convert.ToBoolean(reader["adminDasboardAccess"]),
                                sellerModeAccess = Convert.ToBoolean(reader["sellerModeAccess"]),
                                viewModeAccess = Convert.ToBoolean(reader["viewModeAccess"]),
                                recordModeAcess = Convert.ToBoolean(reader["recordModeAcess"]),
                            };
                            roles.Add(product);
                        }
                    }
                }

                connection.Close();
            }

            return roles;
        }

        public List<User> getUsers()
        {
            List<User> users = new List<User>();

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM users";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                id = Convert.ToInt32(reader["id"]),
                                user = Convert.ToString(reader["user"]),
                                password = Convert.ToString(reader["password"]),
                                role = Convert.ToString(reader["role"]),
                            };
                            users.Add(user);
                        }
                    }
                }

                connection.Close();
            }

            return users;
        }

        public void updateUser(User user)
        {
            var connection = getConnection();

            string query = "UPDATE users SET user = @user, role = @role WHERE id = @id";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", user.user);
                    command.Parameters.AddWithValue("@role", user.role);
                    command.Parameters.AddWithValue("@id", user.id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void removeUser(int id)
        {
            var connection = getConnection();

            string query = "DELETE FROM users WHERE id = @id";
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

        public void updateRole(Role role)
        {
            var connection = getConnection();

            string query = "UPDATE roles SET adminDasboardAccess = @adminDasboardAccess, sellerModeAccess = @sellerModeAccess, viewModeAccess = @viewModeAccess, recordModeAcess = @recordModeAcess WHERE role = @role";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminDasboardAccess", role.adminDasboardAccess);
                    command.Parameters.AddWithValue("@sellerModeAccess", role.sellerModeAccess);
                    command.Parameters.AddWithValue("@viewModeAccess", role.viewModeAccess);
                    command.Parameters.AddWithValue("@recordModeAcess", role.recordModeAcess);
                    command.Parameters.AddWithValue("@role", role.role);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void removeRole(string role)
        {
            var connection = getConnection();

            string query = "DELETE FROM roles WHERE role = @role";
            using (connection)
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@role", role);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void addRole(Role role)
        {
            var connection = getConnection();
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO roles VALUES (@role, @adminDasboardAccess, @sellerModeAccess, @viewModeAccess, @recordModeAcess)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@role", role.role);
                    command.Parameters.AddWithValue("@adminDasboardAccess", role.adminDasboardAccess);
                    command.Parameters.AddWithValue("@sellerModeAccess", role.sellerModeAccess);
                    command.Parameters.AddWithValue("@viewModeAccess", role.viewModeAccess);
                    command.Parameters.AddWithValue("@recordModeAcess", role.recordModeAcess);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
