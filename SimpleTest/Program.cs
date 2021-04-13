using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMath;
using VMath.Fur;
using VMath.Neu;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //IList<double> x = new List<double>() { 0, 1, 6, 12 };
            //IList<double> y = new List<double>() { 8, 10, 7, 0 };
            //ABSolver aBSolver = new ABSolver(x, y, 23);
            //FXFY res = aBSolver.Solve(1);
            //using (StreamWriter file = new StreamWriter(@"C:\dell\test.txt"))
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        file.WriteLine(res.FX[i] + ";" + res.FY[i]);
            //    }
            //}
            //FurSolver furSolver = aBSolver.GetSolver();
            //double yj = furSolver.CalcY(2234243);
            LStruct lStruct = new LStruct();
            FirstLayer firstLayer = new FirstLayer();

            IList<double> in1 = new List<double>() { 5, 21, 33, 37, 4, 44, 1 };
            IList<double> teach1 = new List<double>() { 13,  34,  48,  5,  10,  4,  3 };

            IList<double> in2 = new List<double>() { 13, 34, 48, 5, 10, 4, 3 };
            IList<double> teach2 = new List<double>() { 23,  27,  40,  11,  18,  43,  39 };

            IList<double> in3 = new List<double>() { 23, 27, 40, 11, 18, 43, 39 };
            IList<double> teach3 = new List<double>() { 7,  18,  19,  20,  1,  46,  12 };

            IList<double> in4 = new List<double>() { 7, 18, 19, 20, 1, 46, 12 };
            IList<double> teach4 = new List<double>() { 39,  13,  48,  33,  8,  30,  22 };

            IList<double> in5 = new List<double>() { 39, 13, 48, 33, 8, 30, 22 };
            IList<double> teach5 = new List<double>() { 3,  9,  32,  36,  45, 6,  46 };

            firstLayer.Outs = in1;
            Layer l1 = new Layer(15, AFType.Logistic);
            lStruct.Pairs.Add(new KeyValuePair<Guid, Guid>(firstLayer.ID, l1.ID));
            lStruct.Layers.Add(l1);
            Layer l2 = new Layer(30, AFType.Logistic);
            lStruct.Pairs.Add(new KeyValuePair<Guid, Guid>(l1.ID, l2.ID));
            lStruct.Layers.Add(l2);
            Layer outl = new Layer(7, AFType.Logistic);
            lStruct.Pairs.Add(new KeyValuePair<Guid, Guid>(l2.ID, outl.ID));
            lStruct.Layers.Add(outl);

            lStruct.Solve(firstLayer);
            lStruct.Training(teach1, outl);

            firstLayer.Outs = in2;
            lStruct.Solve(firstLayer);
            lStruct.Training(teach2, outl);

            firstLayer.Outs = in3;
            lStruct.Solve(firstLayer);
            lStruct.Training(teach3, outl);

            firstLayer.Outs = in4;
            lStruct.Solve(firstLayer);
            lStruct.Training(teach4, outl);

            firstLayer.Outs = in5;
            lStruct.Solve(firstLayer);
            lStruct.Training(teach5, outl);

            Console.ReadKey();  
        }
    }
}
