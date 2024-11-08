using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using UTS_PBO.Models;

namespace UTS_PBO.Models
{
    internal class LoginAdminContext
    {
        private readonly string connStr;
        public LoginAdminContext()
        {
            connStr = "Server=localhost;Port=5432;Username=postgres;Password=310305;Database=BonsaRental;CommandTimeout=10";
        }
        public LoginAdmin Validate(string email, string password)
        {
            LoginAdmin loginAdmin = null;
            string query = "SELECT * FROM admin WHERE email = @email AND password = @password";
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loginAdmin = new LoginAdmin(email, password);
                            loginAdmin.Email = (string)reader["email"];
                            loginAdmin.Password = (string)reader["password"];
                        }
                    }
                }
            }
            return loginAdmin;
        }
    }
}

