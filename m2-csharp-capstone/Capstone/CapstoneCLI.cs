using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using System.Configuration;

namespace Capstone
{
    public class CapstoneCLI
    {
        private string connectionString;

        public CapstoneCLI()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }

        public void RunCLI()
        {
            ParkMenu();
        }

        public void ParkMenu()
        {
            Console.WriteLine("National Parks");
            Console.WriteLine();
            ParkSqlDAL parkSql = new ParkSqlDAL(connectionString);
            List<Park> parkList = parkSql.GetAllParks();
            int parkChoice;
            bool done = false;

            for (int i = 1; i <= parkList.Count; ++i)
            {
                // print i + park.name;
                Console.WriteLine($"{i}) {parkList[i - 1].Name}");
            }

            Console.WriteLine("0) quit");

            while (!done)
            {
                Console.WriteLine();
                Console.Write("Choice: ");

                
                bool isNumeric = int.TryParse(Console.ReadLine(), out parkChoice);

                while (!isNumeric || parkChoice > parkList.Count|| parkChoice <0)
                {
                    Console.Write("Please enter a valid selection: ");
                    isNumeric = int.TryParse(Console.ReadLine(), out parkChoice);
                    Console.WriteLine();

                    
                }

                if (parkChoice <= parkList.Count && parkChoice>0)
                {
                    CMDMenu(parkChoice);
                   
                }
                else if (parkChoice == 0)
                {
                    Environment.Exit(0);
                }
            }
        }
        public void CMDMenu(int parkChoice)
        {
            ParkSqlDAL parkSql = new ParkSqlDAL(connectionString);
            Park park = parkSql.GetParkInfo(parkChoice);
            string cmdChoice;
            bool done = false;
            Console.WriteLine("\n" + park.ToString() + "\n");

            while (!done)
            {
                Console.WriteLine();
                Console.Write($"Select a Command" +
                $"\n\t1) View Campgrounds" +
                $"\n\t2) Search for Reservation" +
                $"\n\t3) Return to Previous Screen" +             
                $"\n\tChoice: ");
                cmdChoice = Console.ReadLine();

                while (cmdChoice != "1" && cmdChoice != "2" && cmdChoice != "3")
                {
                    Console.Write("Please enter a valid selection: ");
                    cmdChoice = Console.ReadLine();
                    Console.WriteLine();
                }

                if (cmdChoice == "1")
                {
                    Console.WriteLine();
                    PrintAllCampgrounds();
                }
                else if (cmdChoice == "2")

                {
                    SearchMenu();
                }
                else if (cmdChoice == "3")
                {
                    ParkMenu();
                }
            }
        }


        public void SearchMenu()
        {
            Console.WriteLine();
            PrintAllCampgrounds();

            Console.WriteLine();
            Console.Write("Which campground (enter 0 to cancel)? ");
            int campgroundNum;
            bool isNumeric = int.TryParse(Console.ReadLine(), out campgroundNum);

            while (!isNumeric || campgroundNum < 0)
            {
                Console.Write("Please enter a valid selection: ");
                isNumeric = int.TryParse(Console.ReadLine(), out campgroundNum);
                Console.WriteLine();


            }

            if (campgroundNum == 0)
            {
                ParkMenu();
            }
            else
            {
                Console.Write("What is the arrival date? ");
                DateTime arrivalDate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("What is the departure date? ");
                DateTime departureDate = Convert.ToDateTime(Console.ReadLine());

                SiteSqlDAL siteDAL = new SiteSqlDAL(connectionString);
                CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
                List<Site> siteList = siteDAL.GetAvailableSites(campgroundNum, arrivalDate, departureDate);

                Console.WriteLine();
                Console.WriteLine("Campground".PadRight(20) + "Site No.".PadRight(15) + "Max Occup.".PadRight(15) +
                    "Accessible?".PadRight(15) + "RV Len".PadRight(15) +"Utility".PadRight(15)+ "Cost".PadRight(15));
                Campground campground = campgroundDAL.GetCampground(campgroundNum);

                foreach (Site site in siteList)
                {
                    //string siteNa = site.Campground_id.ToString();
                    string siteNo = site.Site_number.ToString();
                    string siteMO = site.Max_occupancy.ToString();
                    string siteA = site.Accessible.ToString();
                    string siteMRV = site.Max_rv_length.ToString();
                    string siteU = site.Utilities.ToString();
                    string campNa = campground.Name.ToString();

                    int lenghtOfStay = (int)(departureDate - arrivalDate).TotalDays;
                    decimal costOfStay = lenghtOfStay * campground.Daily_fee;
                    string costOfStayString = costOfStay.ToString("C");

                    Console.WriteLine(campNa.PadRight(20)+ siteNo.PadRight(20) + siteMO.PadRight(15) + siteA.PadRight(10) + siteMRV.PadRight(15) + siteU.PadRight(15) +costOfStayString.PadRight(5));
                
                }
                ReservationMenu(arrivalDate, departureDate);
            }
        }

        public void ReservationMenu(DateTime arrivalDate, DateTime departureDate)
        {
            Console.WriteLine();
            Console.Write("Which site should be reserved (enter 0 to cancel)? ");
            int siteChoice = Convert.ToInt32(Console.ReadLine());
            string customerName = "";
            if (siteChoice == 0)
            {
                SearchMenu();
            }
            else
            {
                Console.Write("What name should the reservation be made under? ");
                customerName = Console.ReadLine();
                ReserveCampsite(siteChoice, customerName, arrivalDate, departureDate);
            }
           
        }

        public void ReserveCampsite(int choice, string name, DateTime arrivalDate, DateTime departureDate)
        {
            ReservationSqlDAL reserveDAL = new ReservationSqlDAL(connectionString);

            int reservation_id = reserveDAL.InsertReservation(choice, name, arrivalDate, departureDate);

            Console.WriteLine("\nThe reservation has been made and the confirmation id is {" + reservation_id + "}");
            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadLine();
        }

        public void PrintAllCampgrounds()
        {
            CampgroundSqlDAL cgDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgroundList = cgDAL.GetAllCampgrounds();

            Console.WriteLine("\n".PadRight(5) + "Name" + "Open".PadLeft(35) +
                "Close".PadLeft(11) + "Daily Fee".PadLeft(20));

            for (int i = 1; i <= campgroundList.Count; ++i)
            {
                // print i + park.name;
                Console.WriteLine($"#{i.ToString().PadRight(3)}{campgroundList[i - 1].Name.ToString().PadRight(35)}" +
                    $"{ConvertMonthToString(campgroundList[i - 1].Open_from_mm).ToString().PadRight(10)}" +
                    $"{ConvertMonthToString(campgroundList[i - 1].Open_to_mm).ToString().PadRight(15)}" +
                    $"{campgroundList[i - 1].Daily_fee.ToString("C2").PadLeft(10)}");
            }
        }

        public string ConvertMonthToString(int monthInt)
        {
            string result = "ERROR";

            switch (monthInt)
            {
                case 1:
                    result = "January";
                    break;
                case 2:
                    result = "February";
                    break;
                case 3:
                    result = "March";
                    break;
                case 4:
                    result = "April";
                    break;
                case 5:
                    result = "May";
                    break;
                case 6:
                    result = "June";
                    break;
                case 7:
                    result = "July";
                    break;
                case 8:
                    result = "August";
                    break;
                case 9:
                    result = "September";
                    break;
                case 10:
                    result = "October";
                    break;
                case 11:
                    result = "November";
                    break;
                case 12:
                    result = "December";
                    break;
            }

            return result;
        }
    }
}
