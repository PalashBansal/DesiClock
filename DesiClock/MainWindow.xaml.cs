using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesiClock
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// form params
        int formWidth = 0;
        int formHeight = 0;
        
        ///miscellaneous declarations
        string currentDirectory="";
        
        /// config file declarations
        const string configFilename= "config.ini";
        string configFilenameFull="";
        string configFileContent_str="";

        public MainWindow()
        {
            InitializeComponent();
            string backgroundImageName = "";
            string foregroundImageName = "";
            string highlighterImageName = "";

            ///read config.ini from same directory of exe
            currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            configFilenameFull = currentDirectory + "\\" + configFilename;
            if (File.Exists(configFilenameFull))
            {
                configFileContent_str = File.ReadAllText(configFilenameFull);
                ///update CUSTOMIZABLE parameters
                ExtractFormParam(ref formWidth, 0);
                ExtractFormParam(ref formHeight, 1);
                ExtractImageParam(ref backgroundImageName, 0);
                ExtractImageParam(ref foregroundImageName, 1);
                ExtractImageParam(ref highlighterImageName, 2);
            }
            else
            {
                System.Windows.MessageBox.Show("Missing configuration file \"" + configFilenameFull + "\"");
                System.Windows.Application.Current.Shutdown();
            }
            ///set form size - important parameter and decides image size(width and height) -- should be in proportion to actual image size
            ///image of (x1*x2) should have form of (n*(x1*x2)) -- (where n can be any value) and (x1 can or cannot be equal to x2)
            ///dimension of image being used could be picked at runtime but sometimes we have smaller image and want it to be stretched in bigger application interface
            ///so the formWidth/formHeight parameters from config.ini
            this.Width = formWidth; this.Height = formHeight; ///form size can be precised to double but here limited to int

            //bottom layer image
            ImageSource backImage = new BitmapImage(new Uri(currentDirectory + "\\" + backgroundImageName));
            ImageSource foreImage = new BitmapImage(new Uri(currentDirectory + "\\" + foregroundImageName));
            ImageSource highImage = new BitmapImage(new Uri(currentDirectory + "\\" + foregroundImageName));

            _backImage.Source = backImage;
            _foreImage.Source = foreImage;
            //_backImage.
            
            //_foreImage.ToolTip
            PictureBox pboxBackImage = new PictureBox();
                
            /*pboxBackImage.Width = this.Width;
            pboxBackImage.Height = Height;
            pboxBackImage.Location = new Point(this.Location.X, this.Location.Y);
            pboxBackImage.Image = backImage;*/


            /*
            //bottom layer image(custom image by user)
            Image backImage = Image.FromFile(currentDirectory + "\\" + backgroundImageName);
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
            Controls.Add(pboxhighlightImage);*/

        }

        private void ExtractFormParam(ref int formDimension, int deciderFlag)
        {
            int fileContent_len = configFileContent_str.Length;
            int form_pos = 0;
            if (0 == deciderFlag)
                form_pos = configFileContent_str.IndexOf("ClockWidth=") + 11;
            else if (1 == deciderFlag)
                form_pos = configFileContent_str.IndexOf("ClockHeight=") + 12;
            if (form_pos == -1)
            {
                string whichValueMissing = (deciderFlag == 0) ? "width" : "height";
                System.Windows.MessageBox.Show("Missing clock size:" + whichValueMissing + "  in config.ini");
                System.Windows.Application.Current.Shutdown();
            }
            string formDimension_str = "";
            while (!configFileContent_str[form_pos].Equals('\r'))
            {
                formDimension_str += configFileContent_str[form_pos];
                if (form_pos < (fileContent_len - 1))
                    ++form_pos;
                else
                    break;
            }
            formDimension = Int16.Parse(formDimension_str);
        }

        private void ExtractImageParam(ref string imageName, int deciderFlag)
        {
            int fileContent_len = configFileContent_str.Length;
            int image_pos = 0;
            if (0 == deciderFlag)
                image_pos = configFileContent_str.IndexOf("BackgroundImage=") + 16;
            else if (1 == deciderFlag)
                image_pos = configFileContent_str.IndexOf("ForegroundImage=") + 16;
            else if (2 == deciderFlag)
                image_pos = configFileContent_str.IndexOf("HighlighterImage=") + 17;
            if (image_pos == -1)
            {
                string whichFileMissing = (deciderFlag == 0) ? "background" : ((deciderFlag == 1) ? "foreground" : "highlighter");
                System.Windows.MessageBox.Show("Missing " + whichFileMissing + " image name in config.ini");
                System.Windows.Application.Current.Shutdown();
            }
            while (!configFileContent_str[image_pos].Equals('\r'))
            {
                imageName += configFileContent_str[image_pos];
                if (image_pos < (fileContent_len - 1))
                    ++image_pos;
                else
                    break;
            }
            if(!File.Exists(currentDirectory + "\\" + imageName))
            {
                System.Windows.MessageBox.Show("Missing image file: " + imageName);
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void SetDesiTime()
        {
            //pick system time
            //set variables as per time
            //map variables as per text location
            //draw rectangles based on variables values, calling DrawRectangle multiple times here.//picureBox, not rectangles
        }

    }
}
