using AxWMPLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace _67Calculator
{
    public partial class EasterEggForm : Form
    {
        private AxWindowsMediaPlayer axWindowsMediaPlayer1;

        public EasterEggForm()
        {
            InitializeComponent();
        }

        private void EasterEggForm_Load(object sender, EventArgs e)
        {
            // Use relative path to the video in the output folder
            string videoPath = Path.Combine(Application.StartupPath, "67.mp4");

            if (!File.Exists(videoPath))
            {
                MessageBox.Show("Video file not found: " + videoPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Set the media file
            axWindowsMediaPlayer1.URL = videoPath;

            // Hide controls for a cleaner look
            axWindowsMediaPlayer1.uiMode = "none";

            // Play automatically
            axWindowsMediaPlayer1.Ctlcontrols.play();

            // Event when media ends
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // 8 = MediaEnded
            if (e.newState == 8)
            {
                this.Close(); // close the form when finished
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasterEggForm));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(-3, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(490, 420);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // EasterEggForm
            // 
            this.ClientSize = new System.Drawing.Size(489, 421);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "EasterEggForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EasterEggForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
