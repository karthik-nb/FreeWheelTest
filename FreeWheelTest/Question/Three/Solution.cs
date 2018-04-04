using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeWheelTest.Question.Three
{
    class Solution : ISolution
    {
        private List<int> marketList;
        private List<int> cellsList;
        private List<MarketPop> marketPopList;

        public void Run()
        {
            GetAllMarketPops();

            GetAllMarkets();

            GetAllCells();

            if (marketPopList.Count < (marketList.Count * cellsList.Count))
            {
                FillMissingMarketPops();
            }
            else
            {
                Console.WriteLine("No records are missing in MARKET_POP table");
            }

            Console.WriteLine("Exiting Solution 3!!! \n");
            Console.WriteLine("=========================\n");
        }

        private void GetAllMarkets()
        {
            using (var freeWheelEntities = new DB.FreeWheelEntities())
            {
                marketList = (from m in freeWheelEntities.MARKET
                              select m.MARKET_ID).ToList();
            }
        }

        private void GetAllCells()
        {
            using (var freeWheelEntities = new DB.FreeWheelEntities())
            {
                cellsList = (from c in freeWheelEntities.CELLS
                              select c.CELL_ID).ToList();
            }
        }

        private void GetAllMarketPops()
        {
            using (var freeWheelEntities = new DB.FreeWheelEntities())
            {
                marketPopList = (from m in freeWheelEntities.MARKET_POP
                                 select new MarketPop { MarketId = m.MARKET_ID, CellsId = m.CELL_ID }).ToList();
            }
        }

        private void FillMissingMarketPops()
        {
            int currentMarketId, currentCellId;
            List<DB.MARKET_POP> missingItems = new List<DB.MARKET_POP>();

            for (int i = 0; i < marketList.Count; i++)
            {
                currentMarketId = marketList[i];

                for (int j = 0; j < cellsList.Count; j++)
                {
                    currentCellId = cellsList[j];

                    if (!marketPopList.Exists(mp => mp.MarketId == currentMarketId && mp.CellsId == currentCellId))
                    {
                        missingItems.Add(new DB.MARKET_POP { MARKET_ID = currentMarketId, CELL_ID = currentCellId });
                    }
                }
            }

            if(missingItems.Count>0)
            {
                InsertMissingItems(missingItems);
            }
        }

        private void InsertMissingItems(List<DB.MARKET_POP> missingItems)
        {
            Console.WriteLine($"Total # of missing records in MARKET_POP table: {missingItems.Count}");
            Console.WriteLine("Press <ENTER> to insert missing data into MARKET_POP table, or press any other ket to exit");

            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Inserting data...");

                using (var freeWheelEntities = new DB.FreeWheelEntities())
                {
                    freeWheelEntities.MARKET_POP.AddRange(missingItems);
                    freeWheelEntities.SaveChanges();
                }

                Console.WriteLine("Data insert completed. Press any key to exit.");
            }
        }
    }
}
