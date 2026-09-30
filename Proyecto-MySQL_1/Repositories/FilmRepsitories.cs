using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using Proyecto_MySQL_1.Models;

namespace Proyecto_MySQL_1.Repositories
{
    internal class FilmRepsitories
    {
        private readonly string _conn;

        public FilmRepsitories(string conexion)
        {
            _conn = conexion;
        }

        public async Task<Film> getFilmById(int id)
        {
            string sql = "select title , description from film where id = @id";

            await using var connection = new MySqlConnection(_conn);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return null;
            }

            return new Film
            {
                Id = id,
                Title = reader.GetString("title"),
                Description = reader.GetString("description"),
            };
        }
    }
}