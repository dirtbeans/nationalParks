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
        private const string SQL_GetAllReservations = @"SELECT TOP FIVE  FROM sites WHERE;"; //uhhhhh
        private const string SQL_InsertReservation = @"INSERT INTO reservation VALUES (@site_id, @name, @from_date, @to_date, @create_date);";
        private const string SQL_GrabReservationNumber = @"SELECT reservation_id FROM reservation WHERE site_id = @site_id AND name = @name AND from_date = @from_date AND to_date = @to_date;";

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

                    while (reader.Read())
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

        public int InsertReservation(int siteNum, string custName, DateTime arrivalDate, DateTime departureDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertReservation, conn);

                    cmd.Parameters.AddWithValue("@site_id", siteNum);
                    cmd.Parameters.AddWithValue("@name", custName);
                    cmd.Parameters.AddWithValue("@from_date", arrivalDate);
                    cmd.Parameters.AddWithValue("@to_date", departureDate);
                    cmd.Parameters.AddWithValue("@create_date", DateTime.Now);

                    cmd.ExecuteNonQuery();


                    cmd = new SqlCommand(SQL_GrabReservationNumber, conn);

                    cmd.Parameters.AddWithValue("@site_id", siteNum);
                    cmd.Parameters.AddWithValue("@name", custName);
                    cmd.Parameters.AddWithValue("@from_date", arrivalDate);
                    cmd.Parameters.AddWithValue("@to_date", departureDate);
                    cmd.Parameters.AddWithValue("@create_date", DateTime.Now);


                    // throwing exception
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        return Convert.ToInt32(reader["reservation_id"]);
                    }

                    return 0;
                    //USE FOR TESTING
                    //int count = cmd.ExecuteNonQuery();

                    //if (count == 1)
                    //{
                    //    return true;
                    //}

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                throw;
            }
        }
    }
}

