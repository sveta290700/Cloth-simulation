using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public interface IForce
    {
        Vector Apply(Point point);

    }
}
