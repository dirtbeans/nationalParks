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

        public CampgroundSqlDAL(string connectionString)
        {
            this.connectionString = connectionString; 
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
                        
                        Campground c = new Campground();
                        
                        c.Name = Convert.ToString(reader["name"]);
                        c.Open_from_mm = Convert.ToInt32(reader["open_from_mn"]); 
                        c.Open_to_mm = Convert.ToInt32(reader["close_from_mn"]); 
                        c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]); 
                      

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
    }
    
}
