using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public class ForceCoefficient
    {
        private float _coefficient;
        private string _forceName;

        public ForceCoefficient(float coefficient, string forceName)
        {
            _coefficient = coefficient;
            _forceName = forceName;
        }
        public float coefficient
        {
            get => _coefficient;
            set => _coefficient = value;
        }
        public string forceName
        {
            get => _forceName;
        }
    }
}
