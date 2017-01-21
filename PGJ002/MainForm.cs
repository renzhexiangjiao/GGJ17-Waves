using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using EyeXFramework;
using EyeXFramework.Forms;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;

namespace PGJ002
{
    public partial class MainForm : Form
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

        public static byte resolutionoption = 0;

        public static Bitmap languagelabel;
        public static Bitmap languageoptionb;

        public static byte languageoption = 1;

        public static Rectangle startbuttonrect, optionsbuttonrect, quitbuttonrect;
        public static Rectangle resolutionlabelrect, resolutionoptionsrect, languagelabelrect, languageoptionsrect, backbuttonrect;

        public static FormsEyeXHost _eyeXHost = new FormsEyeXHost();

        // GRA
        public static AnimatedSprite bSandMaker;
        public static AnimatedSprite bBambooFarmer;
        public static AnimatedSprite bCalciumMine;
        public static AnimatedSprite bIronForge;
        public static Bitmap bHouseLarge;
        public static Bitmap bHouseMedium;
        public static Bitmap bHouseSmall;

        public static int width = Screen.PrimaryScreen.Bounds.Width, height = Screen.PrimaryScreen.Bounds.Height;
        public static bool fullscreenOn = false;

        public int __width
        {
            get
            {
                return this.Size.Width;
            }
        }
        public int __height
        {
            get
            {
                return this.Size.Height;
            }
        }

        public static bool useEyeTracker;
        GazePointDataStream lightlyFilteredGazeDataStream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
        public static double eyeTrackerX;
        public static double eyeTrackerY;

        public int __cursorX
        {
            get
            {
                if (!useEyeTracker)
                    return Cursor.Position.X - this.Location.X;
                else
                    return (int)eyeTrackerX - this.Location.X;
            }
        }
        public int __cursorY
        {
            get
            {
                if (!useEyeTracker)
                    return Cursor.Position.Y - this.Location.Y;
                else
                    return (int)eyeTrackerY - this.Location.Y;
            }
        }

        public static bool menu = true;
        public static bool options = false;
        public static bool ingame = false;

        public static Timer waveTimer = new Timer();
        public static Timer gameTimer = new Timer();
        public static Timer animTimer = new Timer();
        public static int counter;
        public static string timer;

        private void PlayClick()
        {
            Sound.PlayASound("click");
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            useEyeTracker = (sender as string == "eyetracker" ? true : false);

            if (menu == true)
            {
                if (startbuttonrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    menu = false;
                    ingame = true;
                    PlayClick();
                    waveTimer.Interval = 1000;
                    gameTimer.Interval = 17;
                    animTimer.Interval = 100;
                    animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
                    gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
                    waveTimer.Tick += new System.EventHandler(this.waveTimer_OnTick);
                    gameTimer.Start();
                    waveTimer.Start();
                    animTimer.Start();
                    this.Refresh();
                }
                else if (optionsbuttonrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    menu = false;
                    options = true;
                    PlayClick();
                    this.Refresh();
                }
                else if (quitbuttonrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    PlayClick();
                    Application.Exit();
                }
            }
            else if (options == true)
            {
                
                if (resolutionoptionsrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    PlayClick();
                    resolutionoption++;
                    resolutionoption = (byte)((int)resolutionoption % 4);
                    RefreshAssets();
                    this.Refresh();
                    this.Refresh();
                }
                else if (languageoptionsrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    PlayClick();
                    Program.lang++;
                    if (Program.lang >= Localization.Language.max)
                        Program.lang = 0;
                    RefreshAssets();
                    this.Refresh();
                    
                }
                else if (backbuttonrect.Contains(new Point(__cursorX, __cursorY)) == true)
                {
                    options = false;
                    menu = true;
                    PlayClick();
                    RefreshAssets();
                    this.Refresh();
                }
            }
        }
        Bitmap gameB;
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Location = new Point(0, 0);
            this.Width = 640;
            this.Height = 480;
            _eyeXHost.Start();
            lightlyFilteredGazeDataStream.Next += (s, e) => { eyeTrackerX = e.X; eyeTrackerY = e.Y; };
            RefreshAssets();
            this.KeyDown += MainForm_KeyDown;
            Music.SetMusic("menu");
            //SoundPlayer s = new SoundPlayer("music/menu.wav");
            //s.PlayLooping();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                Form1_Click("eyetracker", null);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
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
                        this.Width = 640;
                        this.Height = 480;
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
            else if (ingame == true)
            {
                //this.DoubleBuffered = false;
                e.Graphics.DrawString(fps.ToString(), new Font(FontFamily.GenericMonospace,
                12.0F, FontStyle.Bold), new SolidBrush(Color.Black), new Point(0, 0));
                
                using (Graphics gameG = Graphics.FromImage(gameB))
                {
                    gameG.FillRectangle(new SolidBrush(Color.Azure), new Rectangle(0, 0, 640, 480));
                    gameG.DrawString(timer, new Font(FontFamily.GenericMonospace,
                12.0F, FontStyle.Bold), new SolidBrush(Color.Black), new Point(55, 0));
                    Point[] t = new Point[] { Tiles.GetTilePointSelection(0, 0), Tiles.GetTilePointSelection(1, 0), Tiles.GetTilePointSelection(1, 1), Tiles.GetTilePointSelection(0, 1) };
                    gameG.DrawPolygon(new Pen(Color.Green),t);
                    gameG.DrawImage(bSandMaker.GetCurrentFrame(), Tiles.GetTilePoint(1,1));
                    gameG.DrawImage(bBambooFarmer.GetCurrentFrame(), Tiles.GetTilePoint(2, 1));
                    gameG.DrawImage(bCalciumMine.GetCurrentFrame(), Tiles.GetTilePoint(2, 2));
                    gameG.DrawImage(bIronForge.GetCurrentFrame(), Tiles.GetTilePoint(1, 2));
                    gameG.DrawImage(bHouseLarge, Tiles.GetTilePoint(0, 0));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(1, 0));
                    gameG.DrawImage(bHouseSmall, Tiles.GetTilePoint(2, 0));
                    gameG.DrawImage(bHouseLarge, Tiles.GetTilePoint(3, 0));
                    gameG.DrawImage(bHouseLarge, Tiles.GetTilePoint(0, 0));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(0, 1));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(0, 2));
                    gameG.DrawImage(bHouseLarge, Tiles.GetTilePoint(0, 3));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(3, 1));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(3, 2));
                    gameG.DrawImage(bHouseLarge, Tiles.GetTilePoint(3, 3));
                    gameG.DrawImage(bHouseMedium, Tiles.GetTilePoint(1, 3));
                    gameG.DrawImage(bHouseSmall, Tiles.GetTilePoint(2, 3));
                    
                }
                var gameRect = new Rectangle(0, 0, __width, __height);
                e.Graphics.DrawImage(gameB, gameRect);
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

            bSandMaker = FileSystem.GetAnimSpriteFromFiles("game/jp_snd_mkr", 4);
            bBambooFarmer = FileSystem.GetAnimSpriteFromFiles("game/jp_bmb_frm", 4);
            bCalciumMine = FileSystem.GetAnimSpriteFromFiles("game/jp_clc_min", 4);
            bIronForge = FileSystem.GetAnimSpriteFromFiles("game/jp_irn_frg", 4);

            bHouseLarge = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_lg"));
            bHouseMedium = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_md"));
            bHouseSmall = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_sm"));
        }
        private void waveTimer_OnTick(object sender, EventArgs e)
        {
            fps = fc % prevfc;
            prevfc = fc;
            counter++;
            if (counter % 20 == 0)
                timer = "0:00";
            else if (counter % 20 > 10)
                timer = "0:0" + (20 - counter % 20).ToString();
            else
                timer = "0:" + (20 - counter % 20).ToString();
            this.Refresh();
        }
        string f;
        int fps;
        int fc;
        int prevfc = 1;
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            bSandMaker.AdvanceFrame();
            bBambooFarmer.AdvanceFrame();
            bCalciumMine.AdvanceFrame();
            bIronForge.AdvanceFrame();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            fc++;
            f = fc.ToString();
            this.Refresh();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            gameB = new Bitmap(640, 480);
            this.Refresh();
        }
    }
}