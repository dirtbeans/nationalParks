using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Capstone.Models;

namespace Capstone.DAL
{
    class SiteSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAvailableSites = @"SELECT TOP 5 s.site_number, s.max_occupancy, s.accessible, s.max_rv_length, s.utilities, s.campground_id, s.site_id FROM site s WHERE(s.campground_id = @campground_id) AND s.site_id IN (SELECT r.site_id FROM reservation r WHERE ((@from_date < r.from_date AND @to_date < r.to_date ) OR (from_date > r.from_date AND to_date > r.to_date )));";

        public SiteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Site> GetAvailableSites(int campgroundNum, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> result = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableSites, conn);

                    cmd.Parameters.AddWithValue("@campground_id", campgroundNum);
                    cmd.Parameters.AddWithValue("@from_date", arrivalDate.ToShortDateString());
                    cmd.Parameters.AddWithValue("@to_date", departureDate.ToShortDateString());

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.Site_id = Convert.ToInt32(reader["site_id"]);
                        s.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_number = Convert.ToInt32(reader["site_number"]);
                        s.Max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible= Convert.ToBoolean(reader["accessible"]);
                        s.Max_rv_length = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        result.Add(s);
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
