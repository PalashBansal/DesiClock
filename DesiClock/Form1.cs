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

namespace DesiClock
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = Properties.Resources.ico;
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;

            //MessageBox.Show("wid:" + Width + "hei:" + Height); initial is 800,450- typical horizontal rectangle
            //form size to be read by .ini in future version
            Width = 500;
            Height = 500;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Width, Height));
            
            //this.BackColor=Color.FromArgb(50,70,100); //30,50,80 is camouflaging; to be read from .ini
            
            //custom by user
            Image mainImage = Properties.Resources.DefaultImageBg;
            PictureBox pboxMainImage = new PictureBox();
            pboxMainImage.Width = Width;
            pboxMainImage.Height = Height;
            pboxMainImage.Image = (Image)mainImage;
            Controls.Add(pboxMainImage);

            SetTime();
        }

        private void SetTime()
        {
            //pick system time
            //set variables as per time
            //map variables as per text location
            //draw rectangles based on variables values, calling DrawRectangle multiple times here.
        }

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
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );



    }
}
