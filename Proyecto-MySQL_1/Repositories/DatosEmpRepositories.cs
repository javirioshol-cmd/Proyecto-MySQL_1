
using MySqlConnector;
using Proyecto_MySQL_1.Models;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_MySQL_1.Repositories
{
    public class DatosEmpRepositories
    {
        private readonly string _connectionString;

        public DatosEmpRepositories()
        {
            // Ajusta aquí tus credenciales de MySQL
            _connectionString = "Server=localhost;Database=sakila;Uid=root;Pwd=1234;";
        }

        public Datos ObtenerEmpleadoPorId(int staffId)
        {
            const string sql = @"
                SELECT staff_id, first_name, last_name, email, store_id, active, username, last_update 
                FROM staff 
                WHERE staff_id = @id 
                LIMIT 1;";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", staffId);
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Datos
                            {
                                StaffId = Convert.ToInt32(reader["staff_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : string.Empty,
                                StoreId = Convert.ToInt32(reader["store_id"]),
                                Active = Convert.ToBoolean(reader["active"]),
                                Username = reader["username"].ToString(),
                                LastUpdate = Convert.ToDateTime(reader["last_update"])
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
