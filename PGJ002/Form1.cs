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
        public static Bitmap h1;

        public static Bitmap startbutton;
        public static Bitmap optionsbutton;
        public static Bitmap quitbutton;
        public static Bitmap backbutton;

        public static Bitmap resolutionlabel;
        public static Bitmap resolutionoption0;
        public static Bitmap resolutionoption1;
        public static Bitmap resolutionoption2;
        public static Bitmap resolutionoption3;

        public static byte resolutionoption = 3;

        public static Bitmap languagelabel;
        public static Bitmap languageoptionb;

        public static byte languageoption = 1;

        public static Rectangle startbuttonrect, optionsbuttonrect, quitbuttonrect;
        public static Rectangle resolutionlabelrect, resolutionoptionsrect, languagelabelrect, languageoptionsrect, backbuttonrect;

        public static int width = Screen.PrimaryScreen.Bounds.Width, height = Screen.PrimaryScreen.Bounds.Height;
        public static bool fullscreenOn = false;

        public static bool menu = true;
        public static bool options = false;
        public static bool ingame = false;

        private void PlayClick()
        {
            Sound.PlayASound("click");
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (menu == true)
            {
                if (startbuttonrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    menu = false;
                    ingame = true;
                    PlayClick();
                }
                else if (optionsbuttonrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    menu = false;
                    options = true;
                    PlayClick();
                    this.Refresh();
                }
                else if (quitbuttonrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    PlayClick();
                    Application.Exit();
                }
            }
            else if (options == true)
            {
                if (resolutionoptionsrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    PlayClick();
                    resolutionoption++;
                    resolutionoption = (byte)((int)resolutionoption % 4);
                    RefreshAssets();
                    this.Refresh();
                }
                else if (languageoptionsrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    PlayClick();
                    Program.lang++;
                    if (Program.lang >= Localization.Language.max)
                        Program.lang = 0;
                    RefreshAssets();
                    this.Refresh();
                    
                }
                else if (backbuttonrect.Contains(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y)) == true)
                {
                    options = false;
                    menu = true;
                    RefreshAssets();
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
            RefreshAssets();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (menu == true)
            {
                startbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                optionsbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                quitbuttonrect = new Rectangle((int)(0.28 * this.Size.Width), (int)(0.6953 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                e.Graphics.DrawImage(startbutton, startbuttonrect);
                e.Graphics.DrawImage(optionsbutton, optionsbuttonrect);
                e.Graphics.DrawImage(quitbutton, quitbuttonrect);
            }
            else if (options == true)
            {
                resolutionlabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                resolutionoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languagelabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languageoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                backbuttonrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.6953 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                e.Graphics.DrawImage(resolutionlabel, resolutionlabelrect);
                switch (resolutionoption)
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
                e.Graphics.DrawImage(languageoptionb, languageoptionsrect);
                e.Graphics.DrawImage(backbutton, backbuttonrect);
            }
        }

        public void RefreshAssets()
        {
            h1 = new Bitmap(FileSystem.GetBitmapFromFile("h1"));

            startbutton = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("startbutton"));
            optionsbutton = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("optionsbutton"));
            quitbutton = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("quitbutton"));
            backbutton = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("backbutton"));

            resolutionlabel = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("resolutionlabel"));
            resolutionoption0 = new Bitmap(FileSystem.GetBitmapFromFile("800x600"));
            resolutionoption1 = new Bitmap(FileSystem.GetBitmapFromFile("1024x768"));
            resolutionoption2 = new Bitmap(FileSystem.GetBitmapFromFile("1280x720"));
            resolutionoption3 = new Bitmap(FileSystem.GetBitmapFromFile("1366x768"));

            languagelabel = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("languagelabel"));
            languageoptionb = new Bitmap(FileSystem.GetLocalizedBitmapFromFile("currentlanguage"));

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}