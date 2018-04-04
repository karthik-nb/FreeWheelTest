using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeWheelTest.Question.Two
{
    class Solution : ISolution
    {
        private List<DB.STATION> stationList;

        public void Run()
        {
            DisplayStations();

            ProcessRequest();

            Console.WriteLine("Exiting Solution 2!!!\n");
            Console.WriteLine("======================\n");
        }

        private void DisplayStations()
        {
            GetStations();

            if (stationList != null && stationList.Count > 0)
            {
                Console.WriteLine("Please enter a station# from the below list, or press <ENTER> to exit");
                Console.WriteLine("(#).  STATIONS");

                for (int i = 0; i < stationList.Count; i++)
                {
                    Console.WriteLine($"({i + 1}). {stationList[i].STATION_NAME}");
                }
            }
            else
            {
                Console.WriteLine("Please load stations data in DB!!!");
            }
        }

        private void GetStations()
        {
            using (var freeWheelEntities = new DB.FreeWheelEntities())
            {
                stationList = (from s in freeWheelEntities.STATION
                                   select s).ToList();

            }
        }

        private void ProcessRequest()
        {
            int stationId = 0;
            while (true)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line)) break;

                if (int.TryParse(line, out stationId))
                {
                    DisplayProgram(stationId);
                }
                else
                {
                    Console.WriteLine("Please input a valid station# !!!");
                }
            }
        }

        private void DisplayProgram(int stationId)
        {
            if (stationId <= stationList.Count)
            {
                var stationName = stationList[stationId - 1].STATION_NAME;

                using (var freeWheelEntities = new DB.FreeWheelEntities())
                {
                    var programDetails = (from p in freeWheelEntities.PROGRAM
                                          join s in freeWheelEntities.STATION on p.STATION_ID equals s.STATION_ID
                                          where s.STATION_NAME.Equals(stationName)
                                          select new { s.STATION_NAME, p.PROGRAM_NAME, p.FLIGHT_DATE })
                                         .GroupBy(g => g.FLIGHT_DATE).OrderByDescending(g=> g.Key).ToList();

                    if (programDetails!=null && programDetails.Count > 0)
                    {
                        var programList = programDetails[0].ToList();

                        programList.Sort((a, b) => a.PROGRAM_NAME.CompareTo(b.PROGRAM_NAME));

                        Console.WriteLine("Program Details");
                        Console.WriteLine("```````````````");

                        Console.WriteLine($"{programList[0].STATION_NAME} || {programList[0].PROGRAM_NAME} || {programList[0].FLIGHT_DATE.Value.ToString("MMM dd, yyyy")}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, no programs found for the requested station");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Sorry, requested station# {stationId} is not available!");
            }
        }
    }
}
