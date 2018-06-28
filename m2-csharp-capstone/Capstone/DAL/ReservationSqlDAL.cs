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
    class ReservationSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllReservations = @"SELECT TOP FIVE  FROM sites WHERE "; //uhhhhh
       

        public ReservationSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reservation> GetAllReservation()
        {
            List<Reservation> result = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllReservations, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())  //is this campground_id passed?
                    {

                        Reservation r = new Reservation();
                        Site s = new Site();
                        Campground c = new Campground();

                        s.Site_number = Convert.ToInt32(reader["park_id"]);
                        s.Accessible = Convert.ToBoolean(reader["name"]);
                        s.Utilities = Convert.ToBoolean(reader["name"]);
                        s.Max_occupancy = Convert.ToInt32(reader["area"]);
                        s.Max_rv_length = Convert.ToInt32(reader["area"]);
                        c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);
                        //cost per stay
                        //daily fee in campgrounds gonna have to do 2 joins

                        result.Add(r);
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

