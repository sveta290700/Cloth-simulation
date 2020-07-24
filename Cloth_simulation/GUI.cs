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

        private Environment environment = new Environment();

        public GUI()
        {
            environment.inputData();
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            environment.tick();
            SolidBrush myBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(myBrush);
            for (int i = 0; i < environment.PointsCollection.Count; i++)
            {
                Point point = environment.PointsCollection[i];
                panel1.CreateGraphics().DrawEllipse(pen, new RectangleF(point.pos.y - 5,
                     point.pos.z - 5, 10, 10));
            }
            
        }
    }
}
