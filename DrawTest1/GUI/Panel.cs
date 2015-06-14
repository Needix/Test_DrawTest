using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DrawTest1.GUI {
    public partial class Panel : System.Windows.Forms.Panel {
        //Const
        private int m_colorChangeSpeed = 10;
        public int ColorChangeSpeed {
            get { return m_colorChangeSpeed; }
            set { m_colorChangeSpeed = value; }
        }

        private Random random = new Random();

        private Color[] colors = new Color[5] { Color.Blue, Color.Red, Color.Black, Color.Green, Color.Yellow };
        public Panel() {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            GUI gui = GUI.getInstance;
            Graphics g = e.Graphics;

            g.Clear(Color.Black);

            Color color = colors[random.Next(colors.Length)];
            g.FillEllipse(new SolidBrush(calculateNextColor()), new Rectangle(0, 0, this.Width - 20, this.Height - 40));
        }

        private int curColorIndex = 0;
        private Color calculateNextColor() {
            if(curColorIndex + m_colorChangeSpeed > 255 * 6)
                curColorIndex = -m_colorChangeSpeed;
            curColorIndex += m_colorChangeSpeed;

            int red = 255;
            int green = 0;
            int blue = 0;

            int rest = curColorIndex % 255;
            int dev = curColorIndex / 255;
            if(dev == 0) { //red,_green
                red = 255;
                green = rest;
                blue = 0;
            } else if(dev == 1) { //_red, green
                red = 255 - rest;
                green = 255;
                blue = 0;
            } else if(dev == 2) { //green, _blue
                red = 0;
                green = 255;
                blue = rest;
            } else if(dev == 3) { //_green, blue
                red = 0;
                green = 255 - rest;
                blue = 255;
            } else if(dev == 4) { //blue, _red
                red = rest;
                green = 0;
                blue = 255;
            } else if(dev == 5) { //blue, _red
                red = 255;
                green = 0;
                blue = 255-rest;
            }

            Color color = Color.FromArgb(red, green, blue);
            return color;
        }
    }
}
