using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTrans
{
    public partial class ImgPreview : Form
    {
        public ImgPreview()
        {
            InitializeComponent();
        }

        public ImgPreview(String title, string file)
        {
            InitializeComponent();
            this.TopMost = true;
            this.Text = title;
            
            if (File.Exists(file))
            {
                Stream stream = new MemoryStream();
                using (FileStream fs = File.OpenRead(file))
                {
                    fs.CopyTo(stream);
                }
                this.pictureBox1.Image = new Bitmap(stream);
            }
            
        }

        private void ImgPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.pictureBox1.Image = null;
            //this.pictureBox1.Image.Dispose();
        }
    }
}
