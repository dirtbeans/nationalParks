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
        public List<Park> GetAllParks()
        {
            List<Park> result = new List<Park>();

            return result;
        }

        public Park GetParkInfo(int id)
        {
            Park result = new Park();

            return result;
        }
    }
}
