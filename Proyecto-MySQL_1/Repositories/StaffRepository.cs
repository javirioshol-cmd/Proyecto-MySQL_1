using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using Proyecto_MySQL_1.Models;

namespace Proyecto_MySQL_1.Repositories
{
	internal class StaffRepository
	{
		private readonly string _conn;

		public StaffRepository(string conexion)
		{
			_conn = conexion;
		}

		// 1. Obtiene los empleados (staff) de Sakila
		public async Task<List<Staff>> GetAllStaffAsync()
		{
			var list = new List<Staff>();
			string sql = "SELECT staff_id, CONCAT(first_name, ' ', last_name) AS full_name FROM staff";

			await using var connection = new MySqlConnection(_conn);
			await connection.OpenAsync();

			await using var command = new MySqlCommand(sql, connection);
			await using var reader = await command.ExecuteReaderAsync();

			while (await reader.ReadAsync())
			{
				list.Add(new Staff
				{
					Id = reader.GetInt32("staff_id"),
					Name = reader.GetString("full_name")
				});
			}

			return list;
		}

		// 2. Cuenta cuántos alquileres (rental) ha realizado un empleado
		public async Task<int> GetRentalCountByStaffIdAsync(int staffId)
		{
			string sql = "SELECT COUNT(*) FROM rental WHERE staff_id = @staffId";

			await using var connection = new MySqlConnection(_conn);
			await connection.OpenAsync();

			await using var command = new MySqlCommand(sql, connection);
			command.Parameters.AddWithValue("@staffId", staffId);

			var result = await command.ExecuteScalarAsync();
			return Convert.ToInt32(result);
		}
	}
}