using System;

namespace VMath.Fur
{
    public class FurSolver
    {
        double a0;
        double[] ax;
        double[] bx;
        double w;
        int po;

        public FurSolver(double a0, double[] ax, double[] bx, int po, double w)
        {
            this.a0 = a0;
            this.ax = ax;
            this.bx = bx;
            this.po = po;
            this.w = w;
        }

        public double CalcY(double y)
        {
            double s = a0 / 2;
            for (int j = 1; j < po; j++)
            {
                s += ax[j - 1] * Math.Cos(j * w * y) + bx[j - 1] * Math.Sin(j * w * y);
            }
            return s;
        }
    }
}
