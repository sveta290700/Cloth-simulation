using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public interface IForce
    {
        Vector Apply(Point point);
    }
}