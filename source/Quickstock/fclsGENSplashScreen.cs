using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace DSMS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class fclsGENSplashScreen : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		
		private System.Windows.Forms.Timer tmrTimer;
        
        //
        // Variables for first splash example
        //
		// Threading
		static fclsGENSplashScreen ms_frmSplash = null;
		static Thread ms_thrThread = null;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private double m_dblOpacityDecrement = .08;
		private System.Windows.Forms.PictureBox pictureBox1;
		private OSProgressCSharp.OSProgress osProgress1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
        private ProgressBar progressBar1;
		private const int TIMER_INTERVAL = 50;

		public fclsGENSplashScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//this.Opacity = .0;
			tmrTimer.Interval = TIMER_INTERVAL;
			tmrTimer.Start();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fclsGENSplashScreen));
            this.tmrTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.osProgress1 = new OSProgressCSharp.OSProgress();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrTimer
            // 
            this.tmrTimer.Tick += new System.EventHandler(this.tmrTimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.MidnightBlue;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-30, -20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 504);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // osProgress1
            // 
            this.osProgress1.AutoProgress = true;
            this.osProgress1.AutoProgressSpeed = 190;
            this.osProgress1.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.osProgress1.Location = new System.Drawing.Point(56, 352);
            this.osProgress1.Name = "osProgress1";
            this.osProgress1.Position = 8;
            this.osProgress1.ShowBorder = false;
            this.osProgress1.Size = new System.Drawing.Size(224, 16);
            this.osProgress1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Loading program...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Version 1.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(288, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quick Stock";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(60, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(220, 20);
            this.progressBar1.TabIndex = 8;
            // 
            // fclsGENSplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 416);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.osProgress1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fclsGENSplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Gainsboro;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		// A static entry point to launch SplashScreen.
		static private void ShowForm()
		{
			ms_frmSplash = new fclsGENSplashScreen();
			Application.Run(ms_frmSplash);
		}
		
		// A static method to close the SplashScreen
		static public void CloseForm()
		{
			if(ms_frmSplash != null)
			{
				// Make it start going away.
				ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
			}
			
			ms_thrThread = null;  // we do not need these any more.
			ms_frmSplash = null;
		}

		static public void ShowSplashScreen()
		{
			// Make sure it is only launched once.
			if(ms_frmSplash != null)
				return;

			ms_thrThread = new Thread(new ThreadStart(fclsGENSplashScreen.ShowForm));
			ms_thrThread.IsBackground = true;
			ms_thrThread.ApartmentState = ApartmentState.STA;
			ms_thrThread.Start();
		}

		private void tmrTimer_Tick(object sender, System.EventArgs e)
		{
			if( m_dblOpacityIncrement > 0 )
			{
				if( this.Opacity < 1 )
					this.Opacity += m_dblOpacityIncrement;
			}
			else
			{
				if( this.Opacity > 0 )
					this.Opacity += m_dblOpacityIncrement;
				else
					this.Close();
			}		
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
		
		}
		
		// A property returning the splash screen instance
		static public fclsGENSplashScreen SplashForm 
		{
			get
			{
				return ms_frmSplash;
			} 
		}
	}
}
