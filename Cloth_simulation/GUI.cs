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
                _graphics.DrawLine(pen, spring.point0.pos.y, spring.point0.pos.z, 
                    spring.point1.pos.y, spring.point1.pos.z);
            }
            
            //отрисовка точек и сил
            /*for (int i = 0; i < environment.PointsCollection.Count; i++)
            {
                Point point = environment.PointsCollection[i];
                float radius = point.getRadius();
                _graphics.DrawEllipse(pen, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));
                _graphics.FillEllipse(grayBrush, new RectangleF(point.pos.y - radius,
                    point.pos.z - radius, 2 * radius, 2 * radius));
                for (int j = 0; j < environment.ForcesCollection.Count; j++)
                {
                    var vect = environment.ForcesCollection[j].Apply(point);
                    _graphics.DrawLine(new Pen(Color.LightCoral), point.pos.y,
                        point.pos.z, point.pos.y + vect.y,
                        point.pos.z + vect.z);
                }
            }*/
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

        private void button1_Click(object sender, EventArgs e)
        {
            string errors = "";
            if (!Checks.IsEmpty(waterMarkTextBox1.Text))
            {
                if (!Checks.IsNumber(waterMarkTextBox1.Text))
                {
                    errors += "Неверный формат данных в поле <Ширина>\n";
                }
                if (Checks.IsNumber(waterMarkTextBox1.Text) && (!Checks.IsValidNumber(Convert.ToInt32(waterMarkTextBox1.Text), 2, 31)))
                {
                    errors += "Значение " + waterMarkTextBox1.Text + " не попадает в допустимый диапазон поля <Ширина> 2..31\n";
                }
            }
            else
            {
                errors += "Пустое значение поля <Ширина>\n";
            }
            if (!Checks.IsEmpty(waterMarkTextBox2.Text))
            {
                if (!Checks.IsNumber(waterMarkTextBox2.Text))
                {
                    errors += "Неверный формат данных в поле <Высота>\n";
                }
                if (Checks.IsNumber(waterMarkTextBox2.Text) && (!Checks.IsValidNumber(Convert.ToInt32(waterMarkTextBox2.Text), 2, 5)))
                {
                    errors += "Значение " + waterMarkTextBox2.Text + " не попадает в допустимый диапазон поля <Высота> 2..5\n";
                }
            }
            else
            {
                errors += "Пустое значение поля <Высота>\n";
            }
            if (!Checks.IsEmpty(waterMarkTextBox3.Text))
            {
                if (!Checks.IsNumber(waterMarkTextBox3.Text))
                {
                    errors += "Неверный формат данных в поле <Булавки>\n";
                }
                if (Checks.IsNumber(waterMarkTextBox3.Text) && (!Checks.IsValidNumber(Convert.ToInt32(waterMarkTextBox3.Text), 1, 10)))
                {
                    errors += "Значение " + waterMarkTextBox3.Text + " не попадает в допустимый диапазон поля <Булавки> 1..10\n";
                }
            }
            else
            {
                errors += "Пустое значение поля <Булавки>\n";
            }
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    pen.Color = Color.Red;
                    break;
                case 2:
                    pen.Color = Color.Blue;
                    break;
                case 3:
                    pen.Color = Color.Green;
                    break;
            }
            if (errors == "")
            {
                environment.CreateCloth(Convert.ToInt32(waterMarkTextBox1.Text), Convert.ToInt32(waterMarkTextBox2.Text), Convert.ToInt32(waterMarkTextBox3.Text));
                waterMarkTextBox1.Enabled = false;
                waterMarkTextBox2.Enabled = false;
                waterMarkTextBox3.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show(errors, "Некорректные входные данные");
            }
        }
    }
}
