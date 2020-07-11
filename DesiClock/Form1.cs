using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace DesiClock
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ///Initialize form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = Properties.Resources.ico;
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;
            BackColor = Color.Lime;// to be tested using red foreground image
            TransparencyKey = Color.Lime;
            //BackgroundImage = myImage;

            ///initialize all FIX parameters to default values


            ///initialize all CUSTOMIZABLE parameters to default values
            int FormWidth = 500;
            int FormHeight = 500;

            ///create config.ini in same directory if doesn't exist
            string currentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string configFilename = "config.ini";
            string configFilenameFull = currentDirectory + "\\" + configFilename;
            if(File.Exists(configFilenameFull))
            {
                ///read file contents and update CUSTOMIZABLE parameters

            }
            else
            {
                ///create file with all CUSTOMIZABLE parameters with default values
                File.Create(configFilenameFull);
                //fill file
            }

            //MessageBox.Show(configFilenameFull);

            //MessageBox.Show("wid:" + Width + "hei:" + Height); //initial is 800,450- typical horizontal rectangle
            //form size to be read by .ini in future version
            Width = FormWidth;
            Height = FormHeight;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Width, Height));
            
            //this.BackColor=Color.FromArgb(50,70,100); //30,50,80 is camouflaging; to be read from .ini
            
            //top layer image, custom image by user
            //Image mainImage = Properties.Resources.DefaultImageBg;
            PictureBox pboxMainImage = new PictureBox();
            pboxMainImage.Width = Width;
            pboxMainImage.Height = Height;
            //pboxMainImage.Image = (Image)mainImage;
            Controls.Add(pboxMainImage);

            //bottom layer image, custom image by user
            //Image backImage = Properties.Resources.backImage;
            PictureBox pboxBackImage = new PictureBox();
            pboxBackImage.Width = Width;
            pboxBackImage.Height = Height;
            pboxBackImage.Location = new Point(this.Location.X, this.Location.Y);
            
            //pboxBackImage.Image = (Image)backImage;
            Controls.Add(pboxBackImage);

            SetTime();
        }

        private void SetTime()
        {
            //pick system time
            //set variables as per time
            //map variables as per text location
            //draw rectangles based on variables values, calling DrawRectangle multiple times here.//picureBox, not rectangles
        }

        /*
        private void DrawRectangle() // to be fixed
        {
            this.Paint += (o, e) => {
                Graphics g = e.Graphics;
                Color solidColor = Color.Black;
                using (Pen selPen = new Pen(solidColor)  )//color by user
                {
                    g.DrawRectangle(selPen, 0, 0, 400, 400);
                    SolidBrush solidBrush = new SolidBrush(solidColor);
                    g.FillRectangle(solidBrush, 0, 0, 400, 400);
                }
            };
        }*/

        /*[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );*/



    }
}
