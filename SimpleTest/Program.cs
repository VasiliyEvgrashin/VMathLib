using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMath;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<double> x = new List<double>() { 0, 1, 6, 12 };
            IList<double> y = new List<double>() { 8, 10, 7, 0 };
            ABSolver aBSolver = new ABSolver(x, y, 23);
            FXFY res = aBSolver.Solve(1);
            using (StreamWriter file = new StreamWriter(@"C:\dell\test.txt"))
            {
                for (int i = 0; i < 10000; i++)
                {
                    file.WriteLine(res.FX[i] + ";" + res.FY[i]);
                }
            }
            FurSolver furSolver = aBSolver.GetSolver();
            double yj = furSolver.CalcY(2234243);
            Console.ReadKey();  
        }
    }
}
