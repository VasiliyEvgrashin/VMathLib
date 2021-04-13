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

        public Neuron(AFType aftype)
        {
            Weights = new List<double>();
            AFType = aftype;
            func = AFDict.GetFunction(aftype);
        }

        void InitWeights(int len)
        {
            for (int i = 0; i < len; i++)
            {
                double w = (double)AFDict.Random.Next(-90, 90) / 100;
                Weights.Add(w);
            }
        }

        public double Activation()
        {
            if (Weights.Count == 0)
            {
                InitWeights(In.Count);
            }
            double s = 0;
            int len = In.Count;
            for (int i = 0; i < len; i++)
            {
                s += In[i] * Weights[i]; 
            }
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
