using System;

namespace VMath.Neu
{
    public enum AFType
    {
        Logistic = 1,
        SemiLinear = 2,
        Liner = 3,
        Gauss = 4,
        th = 5,
        arctg = 6,
        sin = 7,
        BentIdentity = 8
    }
    public class AFDict
    {
        public static Random Random = new Random();
        public static Func<double, double> GetFunction(AFType aftype)
        {
            switch (aftype)
            {
                case AFType.Gauss:
                    return (d) => {
                        return Math.Exp(-(d * d));
                    };
                case AFType.Liner:
                    return (d) => {
                        return d;
                    };
                case AFType.SemiLinear:
                    return (d) => {
                        return (d < 0) ? 0 : d;
                    };
                case AFType.th:
                    return (d) => {
                        return (Math.Exp(d) - Math.Exp(-d)) / (Math.Exp(d) + Math.Exp(-d));
                    };
                case AFType.arctg:
                    return (d) => {
                        return Math.Pow(Math.Tan(d), -1);
                    };
                case AFType.sin:
                    return (d) => {
                        return Math.Sin(d);
                    };
                case AFType.BentIdentity:
                    return (d) => {
                        return ((Math.Sqrt(d * d + 1) - 1) / 2) + d;
                    };
                case AFType.Logistic:
                default:
                    return (d) => {
                        return 1 / (1 + Math.Exp(-d));
                    };
            }
        }

        internal static Func<double, double> GetPFunction(AFType aftype)
        {
            switch (aftype)
            {
                case AFType.Gauss:
                    return (d) => {
                        return -2 * d * Math.Exp(-(d * d));
                    };
                case AFType.Liner:
                    return (d) => {
                        return 1;
                    };
                case AFType.SemiLinear:
                    return (d) => {
                        return (d < 0) ? 0 : 1;
                    };
                case AFType.th:
                    return (d) => {
                        return 1 - d * d;
                    };
                case AFType.arctg:
                    return (d) => {
                        return 1 / (d * d + 1);
                    };
                case AFType.sin:
                    return (d) => {
                        return Math.Cos(d);
                    };
                case AFType.BentIdentity:
                    return (d) => {
                        return (d / (2 * Math.Sqrt(d * d +1))) + 1;
                    };
                case AFType.Logistic:
                default:
                    return (d) => {
                        return d * (1 - d);
                    };
            }
        }
    }
}
