using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public interface IForce
    {
        ForceCoefficient forceCoefficient { get; set; }
        Vector Apply(Point point);
    }
}