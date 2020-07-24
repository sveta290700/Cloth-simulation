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
        private Graphics _graphics;

        public GUI()
        {
            environment.inputData();
            InitializeComponent();
            _graphics = panel1.CreateGraphics();
            timer1.Start();
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            environment.tick();
            SolidBrush myBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(myBrush);
            SolidBrush backBrush = new SolidBrush(Color.White);
            _graphics.FillRectangle(backBrush, 0, 0, panel1.Width, panel1.Height); 
            for (int i = 0; i < environment.PointsCollection.Count; i++)
            {
                Point point = environment.PointsCollection[i];
                _graphics.DrawEllipse(pen, new RectangleF(point.pos.y - 5,
                    point.pos.z - 5, 10, 10));
            }
        }
    }
}
