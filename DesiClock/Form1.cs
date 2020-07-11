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
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Width, Height));
            ///Initialize form properties
            //this.FormBorderStyle = FormBorderStyle.None;//should not display border/titlebar
            this.Icon = Properties.Resources.ico;
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = this.BackColor;
            //BackColor = Color.Transparent;

            int formWidth = 0;
            int formHeight = 0;
            string backgroundImageName = "";
            string foregroundImageName = "";
            string highlighterImageName = "";


            ///read config.ini from same directory of exe
            string currentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string configFilename = "config.ini";
            string configFilenameFull = currentDirectory + "\\" + configFilename;
            if(File.Exists(configFilenameFull))
            {
                ///read file contents and update CUSTOMIZABLE parameters
                ExtractFormParam(configFilenameFull, ref formWidth, 0);
                ExtractFormParam(configFilenameFull, ref formHeight, 1);
                ExtractImageParam(configFilenameFull, ref backgroundImageName, 0);
                ExtractImageParam(configFilenameFull, ref foregroundImageName, 1);
                ExtractImageParam(configFilenameFull, ref highlighterImageName, 2);
            }
            else
            {
                MessageBox.Show("Missing configuration file \"" + configFilenameFull + "\"");
            }

            this.ClientSize = new System.Drawing.Size(formWidth, formHeight);

            //bottom layer image(custom image by user)
            Image backImage = Image.FromFile( currentDirectory + "\\" + backgroundImageName);
            PictureBox pboxBackImage = new PictureBox();
            pboxBackImage.Width = Width;
            pboxBackImage.Height = Height;
            pboxBackImage.Location = new Point(this.Location.X, this.Location.Y);
            pboxBackImage.Image = backImage;

            //top layer image, custom image by user
            Image foreImage = Image.FromFile(currentDirectory + "\\" + foregroundImageName);
            PictureBox pboxForeImage = new PictureBox();
            pboxForeImage.Width = Width;
            pboxForeImage.Height = Height;
            pboxForeImage.Location = new Point(this.Location.X, this.Location.Y);
            pboxForeImage.Image = foreImage;
            pboxForeImage.BackColor = Color.Transparent;

            //top layer image, custom image by user
            Image highlightImage = Image.FromFile(currentDirectory + "\\" + highlighterImageName);
            PictureBox pboxhighlightImage = new PictureBox();
            pboxhighlightImage.Width = Width;//change after calculating size of overlapping box
            pboxhighlightImage.Height = Height;//change after calculating size of overlapping box
            pboxhighlightImage.Location = new Point(this.Location.X, this.Location.Y);
            pboxhighlightImage.Image = highlightImage;

            Controls.Add(pboxForeImage);
            Controls.Add(pboxBackImage);
            Controls.Add(pboxhighlightImage);

            ///draw image using GDI+ to remove edge glitches
            Graphics g = this.CreateGraphics();
            g.DrawImage(highlightImage, new Point(100,100));
            this.Invalidate();// calls paint method, paint method should graphics object and call drawImage
            globalImage = foreImage;
            SetTime();
        }

        Image globalImage;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(globalImage, this.Location);
        }

        private void ExtractFormParam(string configFilenameFull, ref int formDimension, int deciderFlag)
        {
            string fileContent_str = File.ReadAllText(configFilenameFull);
            int fileContent_len = fileContent_str.Length;
            int form_pos = 0;
            if (0 == deciderFlag)
                form_pos = fileContent_str.IndexOf("ClockWidth=") + 11;
            else if (1 == deciderFlag)
                form_pos = fileContent_str.IndexOf("ClockHeight=") + 12;
            if (form_pos == -1)
            {
                string whichValueMissing = (deciderFlag == 0) ? "width" : "height";
                MessageBox.Show("Missing clock size:" + whichValueMissing + "  in config.ini");
            }
            string formDimension_str="";
            while (!fileContent_str[form_pos].Equals('\r'))
            {
                formDimension_str += fileContent_str[form_pos];
                if (form_pos < (fileContent_len - 1))
                    ++form_pos;
                else
                    break;
            }
            formDimension = Int16.Parse(formDimension_str);
        }

        private void ExtractImageParam(string configFilenameFull, ref string imageName, int deciderFlag)
        {
            string fileContent_str = File.ReadAllText(configFilenameFull);
            int fileContent_len = fileContent_str.Length;
            int image_pos=0;
            if(0==deciderFlag)
                image_pos = fileContent_str.IndexOf("BackgroundImage=") + 16;
            else if(1==deciderFlag)
                image_pos = fileContent_str.IndexOf("ForegroundImage=") + 16;
            else if(2==deciderFlag)
                image_pos = fileContent_str.IndexOf("HighlighterImage=") + 17;
            if (image_pos == -1)
            {
                string whichFileMissing = (deciderFlag == 0) ? "background" : ((deciderFlag==1) ? "foreground" : "highlighter");
                MessageBox.Show("Missing " + whichFileMissing + " image name in config.ini");
            }
            while(!fileContent_str[image_pos].Equals('\r'))
            {
                imageName += fileContent_str[image_pos];
                if (image_pos < (fileContent_len-1))
                    ++image_pos;
                else
                    break;
            }
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
