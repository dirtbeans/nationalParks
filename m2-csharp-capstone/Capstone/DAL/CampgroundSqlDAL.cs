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
    public class CampgroundSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllCampgrounds = @"SELECT * FROM campground";
        private const string SQL_GetCampground = @"SELECT * FROM campground WHERE campground_id = @campground_id";

        public CampgroundSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Campground GetCampground(int campground_id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetCampground, conn);
                cmd.Parameters.AddWithValue("@campground_id", campground_id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return MapResultToCampground(reader);
                }
            }
            return null;
        }

        public List<Campground> GetAllCampgrounds()
        {
            List<Campground> result = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllCampgrounds, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = MapResultToCampground(reader);
                        result.Add(c);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        private Campground MapResultToCampground(SqlDataReader reader)
        {
            Campground c = new Campground();

            c.Campground_id = Convert.ToInt32(reader["campground_id"]);
            c.Name = Convert.ToString(reader["name"]);
            c.Open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
            c.Open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
            c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);

            return c;
        }
    }

}
