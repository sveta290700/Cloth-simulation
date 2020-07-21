using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public class DragForce : IForce
    {
        public const double drag = 0.1;
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            result = point.vel * 0.5 * drag;
            return result;
        }
    }
}
