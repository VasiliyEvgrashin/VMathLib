using System;
using System.Collections.Generic;

namespace VMath.Neu
{
    public class Neuron
    {
        const double nu = 0.1;
        public double Error { get; set; }
        public IList<double> In { get; set; }
        public IList<double> Weights { get; set; }
        public AFType AFType { get; }

        Func<double, double> func;
        Func<double, double> pfunc;

        public Func<double, double> Pfunc {
            get
            { 
                if (AFType == AFType.th || AFType == AFType.Logistic)
                {
                    return pfunc;
                } else
                {
                    return (d) => {
                        double s = Summ();
                        return pfunc(s);
                    };
                }
            }
        }

        public Neuron(AFType aftype)
        {
            Weights = new List<double>();
            AFType = aftype;
            func = AFDict.GetFunction(aftype);
            pfunc = AFDict.GetPFunction(aftype);
        }

        void InitWeights(int len)
        {
            for (int i = 0; i < len; i++)
            {
                double w = (double)AFDict.Random.Next(-90, 90) / 100;
                Weights.Add(w);
            }
        }

        public double Summ()
        {
            double s = 0;
            int len = In.Count;
            for (int i = 0; i < len; i++)
            {
                s += In[i] * Weights[i];
            }
            return s;
        }

        public double Activation()
        {
            if (Weights.Count == 0)
            {
                InitWeights(In.Count);
            }
            double s = Summ();
            return func(s);
        }

        internal void FixError()
        {
            int len = In.Count;
            for (int i = 0; i < len; i++)
            {
                double dw = -nu * Error * In[i];
                Weights[i] = Weights[i] + dw;
            }
        }
    }
}
