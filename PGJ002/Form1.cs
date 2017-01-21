using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGJ002
{
    public partial class Form1 : Form
    {
        public static Bitmap h1 = new Bitmap(Bitmap.FromFile("h1.png"));

        public static Bitmap startbutton = new Bitmap(Bitmap.FromFile("startbutton.png")); 
        public static Bitmap optionsbutton = new Bitmap(Bitmap.FromFile("optionsbutton.png"));
        public static Bitmap quitbutton = new Bitmap(Bitmap.FromFile("quitbutton.png"));

        public static Bitmap resolutionlabel = new Bitmap(Bitmap.FromFile("resolutionlabel.png"));
        public static Bitmap resolutionoption0 = new Bitmap(Bitmap.FromFile("800x600.png"));
        public static Bitmap resolutionoption1 = new Bitmap(Bitmap.FromFile("1024x768.png"));
        public static Bitmap resolutionoption2 = new Bitmap(Bitmap.FromFile("1280x720.png"));
        public static Bitmap resolutionoption3 = new Bitmap(Bitmap.FromFile("1366x768.png"));

        public static byte resolutionoption = 3;

        public static Bitmap languagelabel = new Bitmap(Bitmap.FromFile("languagelabel.png"));
        public static Bitmap languageoption0 = new Bitmap(Bitmap.FromFile("polski.png"));
        public static Bitmap languageoption1 = new Bitmap(Bitmap.FromFile("english.png"));
        public static Bitmap languageoption2 = new Bitmap(Bitmap.FromFile("zhongwen.png"));

        public static byte languageoption = 1;

        public static Rectangle startbuttonrect, optionsbuttonrect, quitbuttonrect;
        public static Rectangle resolutionlabelrect, resolutionoptionsrect, languagelabelrect, languageoptionsrect;

        public static int width = Screen.PrimaryScreen.Bounds.Width, height = Screen.PrimaryScreen.Bounds.Height;
        public static bool fullscreenOn = false;

        public static bool menu = true;
        public static bool options = false;

        private void Form1_Click(object sender, EventArgs e)
        {
            if(menu==true)
            {
                if(startbuttonrect.Contains(Cursor.Position)==true)
                {
                    menu = false;
                }
                else if (optionsbuttonrect.Contains(Cursor.Position) == true)
                {
                    menu = false;
                    options = true;
                    this.Refresh();
                }
                else if (quitbuttonrect.Contains(Cursor.Position) == true)
                {
                    Application.Exit();
                }
            }
            else if(options==true)
            {
                if(resolutionoptionsrect.Contains(Cursor.Position) == true)
                {
                    resolutionoption++;
                    resolutionoption =(byte)((int)resolutionoption % 4);
                    this.Refresh();
                }
                else if(languageoptionsrect.Contains(Cursor.Position) == true)
                {
                    languageoption++;
                    languageoption = (byte)((int)languageoption % 3);
                    this.Refresh();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            this.Width = width;
            this.Height = height;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(menu==true)
            {
                startbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                optionsbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                quitbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.6953 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                e.Graphics.DrawImage(startbutton, startbuttonrect);
                e.Graphics.DrawImage(optionsbutton, optionsbuttonrect);
                e.Graphics.DrawImage(quitbutton, quitbuttonrect);
            }
            else if(options==true)
            {
                resolutionlabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                resolutionoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languagelabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languageoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                e.Graphics.DrawImage(resolutionlabel, resolutionlabelrect);
                switch(resolutionoption)
                {
                    case 0:
                        e.Graphics.DrawImage(resolutionoption0, resolutionoptionsrect);
                        this.Width = 800;
                        this.Height = 600;
                        break;
                    case 1:
                        e.Graphics.DrawImage(resolutionoption1, resolutionoptionsrect);
                        this.Width = 1024;
                        this.Height = 768;
                        break;
                    case 2:
                        e.Graphics.DrawImage(resolutionoption2, resolutionoptionsrect);
                        this.Width = 1280;
                        this.Height = 720;
                        break;
                    case 3:
                        e.Graphics.DrawImage(resolutionoption3, resolutionoptionsrect);
                        this.Width = 1366;
                        this.Height = 768;
                        break;
                }
                e.Graphics.DrawImage(languagelabel, languagelabelrect);
                switch(languageoption)
                {
                    case 0:
                        e.Graphics.DrawImage(languageoption0, languageoptionsrect);
                        break;
                    case 1:
                        e.Graphics.DrawImage(languageoption1, languageoptionsrect);
                        break;
                    case 2:
                        e.Graphics.DrawImage(languageoption2, languageoptionsrect);
                        break;
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
