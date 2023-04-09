using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DSMS
{
    public partial class fclsGENSplashScreen2 : Form
    {
        private const double mc_dblOpacityIncrement = .05;
        private const double mc_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;
        private int m_intTimerInterval_ms = 0;              // interval of the timer in milliseconds. This will determine how often the screen is updated
        private int m_NUpdates = 0;                         // number of times the timer triggered the Draw function. This is used in conjunction with timerInterval_ms to determine the duration of the splash screen
        private double m_dblOpacityChange;
        System.Threading.Timer splashTimer = null;          // represents the timer that will be used to signal the form that it is time to update the screen with a new draw

        public fclsGENSplashScreen2(int timerInterval)
        {
            m_intTimerInterval_ms = timerInterval;

            Assembly asm = Assembly.GetExecutingAssembly();

            InitializeComponent();

            m_dblOpacityChange = mc_dblOpacityIncrement;
            this.Opacity = .0;
            this.BackgroundImage = Image.FromStream(asm.GetManifestResourceStream("DSMS.splash.png"));
            tmrTimer.Interval = TIMER_INTERVAL;
            tmrTimer.Start();
        }
        
        //The GetUpMilliseconds method returns the amount of time, in milliseconds, that the splash screen has been active. This is by no means exact and the resolution is only as good as the timer but it is close enough for its purposes. The function determines the time from the number of times the timer was triggered and the interval of the timer.
        public int GetUpMilliseconds()
        {
            return m_NUpdates * m_intTimerInterval_ms;
        }

        private void fclsGENSplashScreen2_Load(object sender, EventArgs e)
        {
             // creates a timer based on the timer interval specified in the constructor. This timer will run on a separate thread and call the overloaded draw function directly.
            System.Threading.TimerCallback splashDelegate = new System.Threading.TimerCallback(this.Draw);
            this.splashTimer = new System.Threading.Timer(splashDelegate, null, m_intTimerInterval_ms, m_intTimerInterval_ms);
        }

        public void KillMe(object o, EventArgs e)
        {
            splashTimer.Dispose();

            m_dblOpacityChange = -mc_dblOpacityDecrement;
        }

        protected void Draw(Object state)
        {
            m_NUpdates++;
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            if (m_dblOpacityChange > 0)
            {
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityChange;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityChange;
                else
                    this.Close();
            }
        }
    }
}
