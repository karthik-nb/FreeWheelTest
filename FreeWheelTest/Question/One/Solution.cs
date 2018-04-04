using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeWheelTest.Question.One
{
    class Solution : ISolution
    {
        private List<string> pgmNameList;

        public void Run()
        {
            FetchData();

            FormatAndSortNames();
            
            var programNames = pgmNameList.Aggregate((current, next) => current + "'," + "'" + next);

            Console.WriteLine("'" + programNames + "'");
            Console.WriteLine("\nExiting Solution 1");
            Console.WriteLine("=========================\n");
        }

        private void FetchData()
        {
            using(var freeWheelEntities = new DB.FreeWheelEntities())
            {
                pgmNameList = (from p in freeWheelEntities.PROGRAM
                               select p.PROGRAM_NAME).ToList();
            }
        }

        private void FormatAndSortNames()
        {
            pgmNameList = pgmNameList.ConvertAll(p => RemoveQuotes(p));
            pgmNameList.Sort();
        }

        private string RemoveQuotes(string value)
        {
            int stringLength = value.Length;
            bool startsWithQuote = value.StartsWith("\"") || value.StartsWith("'");
            bool endsWithQuote = value.EndsWith("\"") || value.EndsWith("'");

            var formattedString = startsWithQuote ? value.Substring(1, (endsWithQuote ? stringLength - 2 : stringLength - 1)) :
                                    endsWithQuote ? value.Substring(0, stringLength - 2) : value;

            return formattedString.Replace("'", "''");
        }
    }
}
