using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        
        private Thread _gameThread;
        private ManualResetEvent _evExit;

        public GUI()
        {
            InitializeComponent();
            DoubleBuffered = true;

            panel1.Visible = false;

            environment.width = panel1.Width;
            environment.height = panel1.Height;
            
            environment.CreateCloth(21, 10, 10);
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _graphics = e.Graphics;
            _graphics.Clear(Color.White);
            
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
                /*_graphics.DrawEllipse(pen, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));
                _graphics.FillEllipse(grayBrush, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));*/
                for (int j = 0; j < environment.ForcesCollection.Count; j++)
                {
                    var vect = environment.ForcesCollection[j].Apply(point);
                    _graphics.DrawLine(new Pen(Color.LightCoral), point.pos.y,
                        point.pos.z, point.pos.y + vect.y,
                        point.pos.z + vect.z);
                }
            }
        }

  
        private void GameThreadProc()
        {
            IAsyncResult tick = null;
            while(!_evExit.WaitOne(40))
            {
                if(tick != null)
                {
                    if(!tick.AsyncWaitHandle.WaitOne(0))
                    {
                        // we are running too slow, maybe we can do something about it
                        if(WaitHandle.WaitAny(
                            new WaitHandle[]
                            {
                                _evExit,
                                tick.AsyncWaitHandle
                            }) == 0)
                        {
                            return;
                        }
                    }
                }
                tick = BeginInvoke(new MethodInvoker(OnGameTimerTick));
            }
        }
        
        private void OnGameTimerTick()
        {
            environment.tick();

            Invalidate();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _evExit = new ManualResetEvent(false);
            _gameThread = new Thread(GameThreadProc);
            _gameThread.Name = "Game Thread";
            _gameThread.Start();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            _evExit.Set();
            _gameThread.Join();
            _evExit.Close();
            base.OnClosed(e);
        }
    }
}
