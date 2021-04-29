using System;
using System.Collections.Generic;
using System.Linq;

namespace VMath.Neu
{
    public class LStruct
    {
        public IList<Layer> Layers { get; }
        public IList<KeyValuePair<Guid, Guid>> Pairs { get; }

        public LStruct()
        {
            Pairs = new List<KeyValuePair<Guid, Guid>>();
            Layers = new List<Layer>();
        }

        public IList<Guid> GetParents(Guid guid)
        {
            return Pairs.Where(s => s.Value == guid).Select(d => d.Key).ToList();
        }
        public IList<Guid> GetChild(Guid guid)
        {
            return Pairs.Where(s => s.Key == guid).Select(d => d.Value).ToList();
        }

        public void Solve(FirstLayer start)
        {
            IList<Guid> child = GetChild(start.ID);
            foreach(Guid guid in child)
            {
                Layer layer = Layers.FirstOrDefault(s => s.ID == guid);
                layer.SolveOuts(start.Outs);

                Solve(layer);
            }
        }

        public void Training(IList<double> etl, Layer outl)
        {
            int len = outl.Outs.Count;
            for (int i = 0; i < len; i++)
            {
                var pf = outl.Neurons[i].Pfunc;
                double err = (outl.Outs[i] - etl[i]) * pf(outl.Outs[i]);
                outl.Neurons[i].Error = err;
            }
            Refl(outl);
            foreach(Layer layer in Layers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    neuron.FixError();
                }
            }
        }

        void Refl(Layer outl)
        {
            IList<Guid> parents = GetParents(outl.ID);
            foreach (Guid guid in parents)
            {
                Layer layer = Layers.FirstOrDefault(s => s.ID == guid);
                if (layer != null)
                {
                    int nl = layer.Neurons.Count;
                    for (int j = 0; j < nl; j++)
                    {
                        double nerr = 0;
                        double y = layer.Outs[j];
                        var pf = layer.Neurons[j].Pfunc;
                        double yd = pf(y);
                        int lnn = outl.Neurons.Count;
                        for (int i = 0; i < lnn; i++)
                        {
                            nerr += (outl.Neurons[i].Error * outl.Neurons[i].Weights[j]) * yd;
                        }
                        layer.Neurons[j].Error = nerr;
                    }
                    Refl(layer);
                }
            }
        }
    }
}
