using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DrawTest1.GUI {
    class RedrawThread {
        //Constants
        private const int SLEEP_TIME = 20;

        //Member
        private Thread m_Thread;

        private Boolean m_Abort = false;
        private Panel panel;

        //Getter
        public Boolean Abort {
            get { return m_Abort; }
            set { m_Abort = value; m_Thread.Interrupt(); }
        }

        ////////////////Functions////////////////
        /////public
        public void start(Panel panel) {
            this.panel = panel;

            ParameterizedThreadStart pts = new ParameterizedThreadStart(run);
            m_Thread = new Thread(pts);
            m_Thread.Name = "ReDrawThread";
            m_Thread.Start();
        }

        /////private
        private void run(Object obj) {
            try { 
                while(!m_Abort) {
                    panel.Invalidate();
                    Thread.Sleep(SLEEP_TIME); 
                }
            } catch(ThreadInterruptedException ex) { }
        }
    }
}
