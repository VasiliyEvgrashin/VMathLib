using System;
using System.Collections.Generic;

namespace VMath.Neu
{

    public class FirstLayer
    {
        public Guid ID { get; }
        public IList<double> Outs { get; set; }

        public FirstLayer()
        {
            ID = Guid.NewGuid();
            Outs = new List<double>();
        }
    }

    public class Layer : FirstLayer
    {
        public IList<Neuron> Neurons { get; set; }

        public Layer(int count, AFType aFType)
        {
            Neurons = new List<Neuron>();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add(new Neuron(aFType));
            }
        }

        public void SolveOuts(IList<double> prevouts)
        {
            Outs = new List<double>();
            int len = Neurons.Count;
            for (int i = 0; i < len; i++)
            {
                Neurons[i].In = prevouts;
                Outs.Add(Neurons[i].Activation());
            }
        }
    }
}
