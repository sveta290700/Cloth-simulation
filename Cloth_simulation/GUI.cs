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
        private SolidBrush backBrush = new SolidBrush(Color.White);
        private SolidBrush grayBrush = new SolidBrush(Color.Gray);
        private Pen grayPen = new Pen(Color.Gray);
        Pen pen = new Pen(Color.Black);

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

            /*if (environment.PointsArray()[1].mass > 0.1)
            {
                environment.PointsArray()[1].mass -= 0.01f;
            }*/
            
            _graphics.FillRectangle(backBrush, 0, 0, panel1.Width, panel1.Height);
            
            for (int i = 0; i < environment.SpringsCollection.Count; i++)
            {
                Spring spring = environment.SpringsCollection[i];
                _graphics.DrawLine(grayPen, spring.point0.pos.y, spring.point0.pos.z, 
                    spring.point1.pos.y, spring.point1.pos.z);
            }
            
            for (int i = 0; i < environment.PointsCollection.Count; i++)
            {
                Point point = environment.PointsCollection[i];
                float radius = point.getRadius();
                _graphics.DrawEllipse(pen, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));
                _graphics.FillEllipse(grayBrush, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));
            }
        }
    }
}
