using System;

namespace VMath.Neu
{
    public enum AFType
    {
        Threshold = 1,
        Signature = 2,
        Logistic = 3,
        SemiLinear = 4,
        Liner = 5,
        RadialBase = 6
    }
    public class AFDict
    {
        public static Random Random = new Random();
        public static Func<double, double> GetFunction(AFType aftype)
        {
            switch (aftype)
            {
                case AFType.RadialBase:
                    return (d) => {
                        return Math.Exp(-(d * d));
                    };
                case AFType.Liner:
                    return (d) => {
                        return d;
                    };
                case AFType.SemiLinear:
                    return (d) => {
                        return (d <= 0) ? 0 : d;
                    };
                case AFType.Signature:
                    return (d) => {
                        return (d <= 0) ? -1 : 1;
                    };
                case AFType.Logistic:
                    return (d) => {

                        return 1 / (1 + Math.Exp(-d));
                    };
                case AFType.Threshold:
                default:
                    return (d) => {
                        return (d < 0) ? 0 : 1;
                    };
            }
        }
    }
}
