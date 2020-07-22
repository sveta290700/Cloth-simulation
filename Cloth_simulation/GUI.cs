using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloth_simulation
{
    public partial class GUI : Form
    {
        public GUI()
        {
            Environment e = new Environment();
            e.inputData();
            e.tick();
            InitializeComponent();
        }
    }
}
