using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_App
{
    public partial class RadarViewerForm : Form
    {
        private PictureBox radarPictureBox;
        public RadarViewerForm(Image radarImage)
        {
            InitializeComponent();

            // Initialize PictureBox
            radarPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = radarImage
            };

            // Add PictureBox to the form
            Controls.Add(radarPictureBox);
        }
    }
}
