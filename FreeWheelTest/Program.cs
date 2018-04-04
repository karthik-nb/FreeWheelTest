using FreeWheelTest.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeWheelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1::
            Console.WriteLine("Solution 1 (question 1)");
            Console.WriteLine("```````````````````````");
            ISolution solution1 = new Question.One.Solution();
            solution1.Run();

            //Question 2::
            Console.WriteLine("Solution 2 (question 2)");
            Console.WriteLine("```````````````````````");
            ISolution solution2 = new Question.Two.Solution();
            solution2.Run();

            //Question 3::
            Console.WriteLine("Solution 3 (question 3)");
            Console.WriteLine("```````````````````````");
            ISolution solution3 = new Question.Three.Solution();
            solution3.Run();

            Console.WriteLine("Press <ENTER> to exit program");
            Console.ReadKey();
        }
    }
}
