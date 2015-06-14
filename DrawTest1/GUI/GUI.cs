using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DrawTest1.GUI {
    public partial class GUI : Form {
        private RedrawThread m_RedrawThread;
        private Panel m_Panel;
        private static GUI m_Instance;
        public static GUI getInstance {
            get {
                if(m_Instance == null)
                    m_Instance = new GUI();
                return m_Instance; 
            }
        }

        private GUI() {
            InitializeComponent();
            InitializeCustomComponents();

            //RedrawThread
            m_RedrawThread = new RedrawThread();
            m_RedrawThread.start(this.m_Panel);
        }

        private void InitializeCustomComponents() {
            //Panel
            m_Panel = new Panel();
            this.m_Panel.Location = new System.Drawing.Point(0, 0);
            this.m_Panel.Name = "m_Panel";
            this.m_Panel.Size = this.Size;
            this.m_Panel.TabIndex = 0;
            this.m_Panel.Text = "Panel";
            this.Controls.Add(m_Panel);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.m_Panel.Size = this.Size;
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            m_RedrawThread.Abort = true;
        }
    }
}
