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
        private const string SQL_GetReservation = @"SELECT s.site_number, s.max_occupancy, s.accessible, s.max_rv_length, s.utilities, s.campground_id, s.site_id FROM site s JOIN reservation r ON s.site_id = r.site_id WHERE(s.campground_id = 1) AND ((@varname1 < r.from_date AND @varname2 < r.to_date ) OR(@varname1 > r.from_date AND @varname2 > r.to_date )) GROUP BY s.site_number";

        public SiteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
