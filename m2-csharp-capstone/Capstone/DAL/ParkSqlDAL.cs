using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;
using System.IO;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllParks = @"SELECT * FROM park ORDER BY park.name";
        private const string SQL_GetParkInfo = @"SELECT * FROM park WHERE park_id = @park_id;";

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString; //Properties.Settings.Default.Connectionstring;
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
                        p.Park_id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.Establish_date = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);

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

                    while(reader.Read())
                    {
                        result.Park_id = Convert.ToInt32(reader["park_id"]);
                        result.Name = Convert.ToString(reader["name"]);
                        result.Location = Convert.ToString(reader["location"]);
                        result.Establish_date = Convert.ToDateTime(reader["establish_date"]);
                        result.Area = Convert.ToInt32(reader["area"]);
                        result.Visitors = Convert.ToInt32(reader["visitors"]);
                        result.Description = Convert.ToString(reader["description"]);
                    } 
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
