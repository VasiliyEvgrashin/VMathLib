using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMath.Fur
{
    public class FRange
    {
        public IList<double> Ak { get; set; }
        public IList<double> Fk { get; set; }

        public FRange()
        {
            Ak = new List<double>();
            Fk = new List<double>();
        }
    }
}
