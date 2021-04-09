using System.Collections.Generic;

namespace VMath.Fur
{
    public class FXFY
    {
        public IList<double> FX { get; set; }
        public IList<double> FY { get; set; }

        public FXFY()
        {
            FX = new List<double>();
            FY = new List<double>();
        }
    }
}
