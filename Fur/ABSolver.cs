using System;
using System.Collections.Generic;

namespace VMath.Fur
{
    public class ABSolver
    {
        int len;
        int po;
        double t;
        double h;
        double w;
        IList<double> x;
        IList<double> y;
        double a0;
        double[] ax;
        double[] bx;

        /// <summary>
        /// initializes the coefficient solver for the Fourier series. must have the same list lengths (x and y).you must start the x coordinates from scratch. do not specify the last point
        /// </summary>
        /// <param name="x">x coordinate pattern</param>
        /// <param name="y">y coordinate pattern</param>
        /// <param name="t">period</param>
        /// <param name="len">trapezium integral calculation step (default = t / 10000)</param>
        /// <param name="po">number of harmonics pairs (default = 5)</param>
        public ABSolver(IList<double> x, IList<double> y, double t, int len = 10000, int po = 5)
        {
            this.len = len;
            this.po = po;
            this.t = t;
            h = t / len;
            w = (2 * Math.PI) / t;
            this.x = x;
            this.y = y;
            if (this.x.Count < t)
            {
                this.x.Add(t);
                this.y.Add(y[0]);
            }
            InitABX();
        }

        /// <summary>
        /// returns an instance to calculate the value of the function
        /// </summary>
        /// <returns></returns>
        public FurSolver GetSolver()
        {
            return new FurSolver(a0, ax, bx, po, w);
        }

        /// <summary>
        /// returns the coordinates of the signal spectrum
        /// </summary>
        /// <returns></returns>
        public FRange GetAkFk()
        {
            FRange result = new FRange();
            int l = ax.Length;
            for(int i = 0; i < l; i++)
            {
                result.Ak.Add(Math.Sqrt(ax[i] * ax[i] + bx[i] * bx[i]));
                result.Fk.Add(Math.Atan(bx[i] / ax[i]));
            }
            return result;
        }

        /// <summary>
        /// returns the point set of the function
        /// </summary>
        /// <param name="per">number of periods</param>
        /// <returns></returns>
        public FXFY Solve(int per = 3)
        {
            FXFY result = new FXFY();
            int ln = len * per;
            for (int i = 0; i < ln; i++)
            {
                double hx = i * h;
                double s = a0 / 2;
                for (int j = 1; j < po; j++)
                {
                    s += ax[j-1] * Math.Cos(j * w * hx) + bx[j-1] * Math.Sin(j * w * hx);
                }
                result.FX.Add(hx);
                result.FY.Add(s);
            }
            return result;
        }

        void InitABX()
        {
            a0 = SolveA(0);
            ax = new double[po];
            bx = new double[po];
            for (int j = 1; j < po; j++)
            {
                ax[j - 1] = SolveA(j);
                bx[j - 1] = SolveB(j);
            }
        }

        double SolveA(double k)
        {
            double s = 0;
            for(int i = 1; i < len; i++)
            {
                double phx = (i - 1) * h;
                X1X2 px1x2 = CheckH(phx);
                double hx = i * h;
                X1X2 x1x2 = CheckH(hx);
                double sy1 = SY(phx, x[px1x2.X1], x[px1x2.X2], y[px1x2.X1], y[px1x2.X2]) * Math.Cos(k * w * phx);
                double sy2 = SY(hx, x[x1x2.X1], x[x1x2.X2], y[x1x2.X1], y[x1x2.X2]) * Math.Cos(k * w * hx);
                s += (2 / t) * (sy1 + sy2) * (h / 2);
            }
            return s;
        }

        double SolveB(double k)
        {
            double s = 0;
            for (int i = 1; i < len; i++)
            {
                double phx = (i - 1) * h;
                X1X2 px1x2 = CheckH(phx);
                double hx = i * h;
                X1X2 x1x2 = CheckH(hx);
                double sy1 = SY(phx, x[px1x2.X1], x[px1x2.X2], y[px1x2.X1], y[px1x2.X2]) * Math.Sin(k * w * phx);
                double sy2 = SY(hx, x[x1x2.X1], x[x1x2.X2], y[x1x2.X1], y[x1x2.X2]) * Math.Sin(k * w * hx);
                s += (2 / t) * (sy1 + sy2) * (h / 2);
            }
            return s;
        }

        X1X2 CheckH(double value)
        {
            int len = x.Count - 1;
            for(int i = 0; i < len; i++)
            {
                if (value >= x[i] && value <= x[i + 1])
                {
                    return new X1X2() { X1 = i, X2 = i + 1 };
                }
            }
            return null;
        }
        double SY(double x, double x1, double x2, double y1, double y2)
        {
            return (x * (y2 - y1) - x1 * (y2 - y1) + y1 * (x2 - x1)) / (x2 - x1);
        }
    }
}

