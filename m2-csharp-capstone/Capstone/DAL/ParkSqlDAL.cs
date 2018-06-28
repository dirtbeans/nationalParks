using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllParks = @"SELECT * FROM park";
        private const string SQL_GetParkInfo = @"SELECT * FROM park WHERE park_id = @park_id;";

        public ParkSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> result = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.park_id = Convert.ToInt32(reader["park_id"]);
                        p.name = Convert.ToString(reader["name"]);
                        p.location = Convert.ToString(reader["location"]);
                        p.establish_date = Convert.ToString(reader["establish_date"]);
                        p.area = Convert.ToInt32(reader["area"]);
                        p.visitors = Convert.ToInt32(reader["visitors"]);
                        p.description = Convert.ToString(reader["description"]);

                        result.Add(p);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public Park GetParkInfo(int id)
        {
            Park result = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkInfo, conn);
                    cmd.Parameters.AddWithValue("@park_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    result.park_id = Convert.ToInt32(reader["park_id"]);
                    result.name = Convert.ToString(reader["name"]);
                    result.location = Convert.ToString(reader["location"]);
                    result.establish_date = Convert.ToString(reader["establish_date"]);
                    result.area = Convert.ToInt32(reader["area"]);
                    result.visitors = Convert.ToInt32(reader["visitors"]);
                    result.description = Convert.ToString(reader["description"]);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }
    }
}
