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

        public static Bitmap opLabel;
        public static Bitmap gameLogo;

        public static AnimatedSprite menuBackground;

        public static Rectangle startbuttonrect, optionsbuttonrect, quitbuttonrect;
        public static Rectangle resolutionlabelrect, resolutionoptionsrect, languagelabelrect, languageoptionsrect, backbuttonrect;

        public static FormsEyeXHost _eyeXHost = new FormsEyeXHost();

        // GRA
        public static AnimatedSprite bWaterWaves;
        public static AnimatedSprite bFireWaves;
        public static AnimatedSprite bEarthquake;
        public static AnimatedSprite bTornado;

        public static AnimatedSprite bSandMaker;
        public static AnimatedSprite bBambooFarmer;
        public static AnimatedSprite bCalciumMine;
        public static AnimatedSprite bIronForge;
        public static Bitmap bUpgrade;
        
        public static Bitmap bHouseLarge;
        public static Bitmap bHouseMedium;
        public static Bitmap bHouseSmall;

        public static Bitmap bBambooWall;
        public static Bitmap bBambooWallFG;
        public static Bitmap bGrassBG;

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
        public int __gameCursorX
        {
            get
            {
                return (int)(__cursorX / (float)((float)this.Width / 800.0f));
            }
        }
        public int __gameCursorY
        {
            get
            {
                return (int)(__cursorY / (float)((float)this.Height / 600.0f));
            }
        }
        public static bool menu = true;
        public static bool options = false;
        public static bool ingame = false;

        public static Timer waveTimer = new Timer();
        public static Timer gameTimer = new Timer();
        public static Timer animTimer = new Timer();
        public static Timer menuTimer = new Timer();
        public static int counter;
        public static string timer;

        private void PlayClick()
        {
            Sound.PlayASound("click_sound_1");
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
                    Random r = new Random();
                    if (r.Next(100) >= 50)
                        Music.SetMusic("gameplay_track_2");
                    else
                        Music.SetMusic("gameplay_track_1");
                    waveTimer.Interval = 1000;
                    gameTimer.Interval = 17;
                    animTimer.Interval = 100;
                    animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
                    gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
                    waveTimer.Tick += new System.EventHandler(this.waveTimer_OnTick);
                    gameTimer.Start();
                    waveTimer.Start();
                    animTimer.Start();
                    menuTimer.Stop();
                    Entity.ResetWorld();
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
            else if(ingame == true)
            {
                if (!lockCursor)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int o = 0; o < 5; o++)
                        {
                            Point[] t = Tiles.GetTilePolygon(i, o);
                            if (Tiles.InsidePolygon(t, new Point(__gameCursorX, __gameCursorY)))
                            {
                                isCursorOnTile = true;
                                tX = i;
                                tY = o;
                            }
                        }
                    }
                }
                if (isCursorOnTile && Entity.population != 0)
                {
                    if (currentMode == Mode.Build && Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY) == null)
                    {
                        Entity.CreateEntity(currentSelection, tX, tY);
                    }
                    else if(currentMode == Mode.Upgrade && Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY && (x.type == EntType.jp_bmb_frm || x.type == EntType.jp_clc_min || x.type == EntType.jp_irn_frg || x.type == EntType.jp_snd_mkr)) != null)
                    {
                        Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY && (x.type == EntType.jp_bmb_frm || x.type == EntType.jp_clc_min || x.type == EntType.jp_irn_frg || x.type == EntType.jp_snd_mkr)).level++;
                    }
                    else if (currentMode == Mode.Bulldoze && Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY) != null)
                    {
                        Entity.entList.RemoveAt(Entity.entList.FindIndex(x => x.PositionX == tX && x.PositionY == tY));
                    }
                    else if (currentMode == Mode.Repair && Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY) != null)
                    {
                        Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY).health += 5;
                        Entity.add_bamboo -= 15;
                    }
                }
            }
        }
        Bitmap gameB;
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Location = new Point(0, 0);
            this.Width = 800;
            this.Height = 600;
            _eyeXHost.Start();
            lightlyFilteredGazeDataStream.Next += (s, e) => { eyeTrackerX = e.X; eyeTrackerY = e.Y; };
            RefreshAssets();
            this.KeyDown += MainForm_KeyDown;
            Music.SetMusic("menu_sound_2_proper");
            menuTimer.Interval = 350;
            menuTimer.Tick += MenuTimer_Tick;
            menuTimer.Start();
            //SoundPlayer s = new SoundPlayer("music/menu.wav");
            //s.PlayLooping();
        }

        private void MenuTimer_Tick(object sender, EventArgs e)
        {
            menuBackground.AdvanceFrame();
            this.Refresh();
        }

        public enum Mode
        {
            Build,
            Repair,
            Upgrade,
            Bulldoze,
            mode_max
        };
        public EntType currentSelection;
        public Mode currentMode;
        public bool lockCursor = false;
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                Form1_Click("eyetracker", null);
            if(ingame)
            {
                if (e.KeyCode == Keys.Escape)
                    Entity.ResetWorld();
                if (Entity.population != 0)
                {
                    if (e.KeyCode == Keys.Right)
                        currentSelection++;
                    if (e.KeyCode == Keys.Left)
                        currentSelection--;

                    if (e.KeyCode == Keys.Up)
                        currentMode++;
                    if (e.KeyCode == Keys.Down)
                        currentMode--;

                    if (e.KeyCode == Keys.ControlKey)
                        lockCursor = !lockCursor;

                    if (currentSelection >= EntType.ent_max)
                    {
                        currentSelection = 0;
                    }
                    if (currentSelection < 0)
                    {
                        currentSelection = EntType.ent_max - 1;
                    }

                    if (currentMode >= Mode.mode_max)
                    {
                        currentMode = 0;
                    }
                    if (currentMode < 0)
                    {
                        currentMode = Mode.mode_max - 1;
                    }
                }
            }
        }
        int lastPosX = 0;
        int lastPosY = 0;
        bool isCursorOnTile = false;
        int tX = 0;
        int tY = 0;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            if (menu == true)
            {
                Rectangle logorect = new Rectangle((int)(0.15 * __width), (int)(0.1 * __height), (int)(0.3 * __height), (int)(0.2 * __height));
                startbuttonrect = new Rectangle((int)(0.1 * this.Size.Width), (int)(0.30 * this.Size.Height), (int)(0.40 * this.Size.Height), (int)(0.20 * this.Size.Height));
                optionsbuttonrect = new Rectangle((int)(0.1 * this.Size.Width), (int)(0.409 * this.Size.Height), (int)(0.40 * this.Size.Height), (int)(0.20 * this.Size.Height));
                quitbuttonrect = new Rectangle((int)(0.1 * this.Size.Width), (int)(0.518 * this.Size.Height), (int)(0.40 * this.Size.Height), (int)(0.20 * this.Size.Height));
                e.Graphics.DrawImage(menuBackground.GetCurrentFrame(), new Rectangle(0, 0, __width, __height));
                e.Graphics.DrawImage(gameLogo, logorect);
                e.Graphics.DrawImage(startbutton, startbuttonrect);
                e.Graphics.DrawImage(optionsbutton, optionsbuttonrect);
                e.Graphics.DrawImage(quitbutton, quitbuttonrect);
                e.Graphics.DrawImage(opLabel, new Point(4, __height - 20));
            }
            else if (options == true)
            {
                resolutionlabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                resolutionoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.04427 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languagelabelrect = new Rectangle((int)(0.04 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                languageoptionsrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.37 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                backbuttonrect = new Rectangle((int)(0.52 * this.Size.Width), (int)(0.6953 * this.Size.Height), (int)(0.44 * this.Size.Width), (int)(0.26 * this.Size.Height));
                e.Graphics.DrawImage(menuBackground.GetCurrentFrame(), new Rectangle(0, 0, __width, __height));
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
            else if (ingame == true)
            {
                //this.DoubleBuffered = false;
                
                
                using (Graphics gameG = Graphics.FromImage(gameB))
                {
                    gameG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    gameG.FillRectangle(new SolidBrush(Color.Azure), new Rectangle(0, 0, 800, 600));
                    gameG.DrawImage(bGrassBG, new Rectangle(0, 0, 800, 600));
                    gameG.DrawImage(bBambooWall, new Rectangle(0, 0, 800, 600));
                    gameG.DrawString(fps.ToString()+ " FPS", new Font(FontFamily.GenericMonospace,
                12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                    //gameG.DrawString(timer, new Font(FontFamily.GenericMonospace,
                    //12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(55, 0));
                    if (!lockCursor)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            for (int o = 0; o < 5; o++)
                            {
                                Point[] t = Tiles.GetTilePolygon(i, o);
                                if (Tiles.InsidePolygon(t, new Point(__gameCursorX, __gameCursorY)))
                                {
                                    isCursorOnTile = true;
                                    tX = i;
                                    tY = o;
                                }
                            }
                        }
                    }
                    if (isCursorOnTile)
                    {
                        Point[] t = Tiles.GetTilePolygon(tX, tY);
                        gameG.DrawPolygon(new Pen(Color.Green, fc % 7), t);
                        if (currentMode == Mode.Build && (fc % 16)>8 && Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY) == null)
                            gameG.DrawImage(Entity.GetSpriteForType(currentSelection), Tiles.GetTilePoint(tX, tY));
                    }
                    Entity selectedEnt = Entity.entList.Find(x => x.PositionX == tX && x.PositionY == tY);
                    for (int p = 0; p < 5; p++) {
                        for (int o = 0; o < 5; o++)
                        {
                            Entity ent = Entity.entList.Find(x => x.PositionX == p && x.PositionY == o);
                            if (ent == null) continue;
                            if (ent.isAnimated)
                            {
                                gameG.DrawImage(ent.animSprite.GetCurrentFrame(), Tiles.GetTilePoint(ent.PositionX, ent.PositionY));
                            }
                            else
                            {
                                gameG.DrawImage(ent.sprite, Tiles.GetTilePoint(ent.PositionX, ent.PositionY));
                            }
                            for (int i = 0; i < ent.level; i++)
                            {
                                gameG.DrawImage(bUpgrade, Tiles.GetTilePointForUpgrade(ent.PositionX, ent.PositionY, i));
                            }
                        }
                    }
                    /*
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
                    */
                    //gameG.DrawImage(bWaterWaves.GetCurrentFrame(), new Rectangle(0, 0, 640, 480));
                    gameG.DrawImage(bBambooWallFG, new Rectangle(0, 0, 800, 600));
                    if (currentDisaster != Disaster.None)
                    {
                        switch (currentDisaster)
                        {
                            case Disaster.Earthquake:
                                gameG.DrawImage(bEarthquake.GetCurrentFrame(), new Rectangle(0, 0, 800, 600));
                                break;
                            case Disaster.Fire:
                                gameG.DrawImage(bFireWaves.GetCurrentFrame(), new Rectangle(0, 0, 800, 600));
                                break;
                            case Disaster.Water:
                                gameG.DrawImage(bWaterWaves.GetCurrentFrame(), new Rectangle(0, 0, 800, 600));
                                break;
                            case Disaster.Wind:
                                gameG.DrawImage(bTornado.GetCurrentFrame(), new Rectangle(0, 0, 800, 600));
                                break;
                        }
                    }
                    //gameG.DrawString("+", new Font(FontFamily.GenericMonospace, 12.0f, FontStyle.Bold), new SolidBrush(Color.White), new Point(__gameCursorX, __gameCursorY));
                    if (Program.lang == Localization.Language.english)
                    {
                        if (Entity.population == 0)
                        {
                            gameG.DrawString("PRESS ESC TO RESTART", new Font(FontFamily.GenericMonospace,
                 16.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 180));
                            gameG.DrawString("GAME\nOVER", new Font(FontFamily.GenericMonospace,
                 72.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 200));
                        }
                        gameG.DrawString("Current building: " + Entity.GetNameForType(currentSelection), new Font(FontFamily.GenericMonospace,
                    12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(110, 0));
                        if (selectedEnt != null && (!useEyeTracker || lockCursor))
                            gameG.DrawString("\nPopulation: " + Entity.population.ToString() + "\nCash: " + Entity.cash.ToString() + " YEN\nBamboo: " + Entity.bamboo.ToString() + " kg\nSand: " + Entity.sand.ToString() + " kg\nLimestone: " + Entity.calcium.ToString() + " kg\nIron: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nMode: " + Enum.GetName(typeof(Mode), currentMode) + "\nSelected building: " + Entity.GetNameForType(selectedEnt.type) + "\nHealth: " + selectedEnt.health.ToString() + "\nLevel: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        else
                            gameG.DrawString("\nPopulation: " + Entity.population.ToString() + "\nCash: " + Entity.cash.ToString() + " YEN\nBamboo: " + Entity.bamboo.ToString() + " kg\nSand: " + Entity.sand.ToString() + " kg\nLimestone: " + Entity.calcium.ToString() + " kg\nIron: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nMode: " + Enum.GetName(typeof(Mode), currentMode) + "\n", new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        if (selectedEnt != null && (useEyeTracker && !lockCursor))
                            gameG.DrawString("Selected building: " + Entity.GetNameForType(selectedEnt.type) + "\nHealth: " + selectedEnt.health.ToString() + "\nLevel: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(lastPosX, lastPosY));
                        else
                        {
                            lastPosX = __gameCursorX;
                            lastPosY = __gameCursorY;
                        }
                    } else if(Program.lang == Localization.Language.polish)
                    {
                        if (Entity.population == 0)
                        {
                            gameG.DrawString("WCIŚNIJ ESC ABY POWTÓRZYĆ", new Font(FontFamily.GenericMonospace,
                 16.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 180));
                            gameG.DrawString("KONIEC\n GRY", new Font(FontFamily.GenericMonospace,
                 72.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 200));
                        }
                        gameG.DrawString("Obecny budynek: " + Entity.GetNameForType(currentSelection), new Font(FontFamily.GenericMonospace,
                12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(110, 0));
                        if (selectedEnt != null && (!useEyeTracker || lockCursor))
                            gameG.DrawString("\nPopulacja: " + Entity.population.ToString() + "\nPieniądze: " + Entity.cash.ToString() + " YEN\nBambus: " + Entity.bamboo.ToString() + " kg\nPiasek: " + Entity.sand.ToString() + " kg\nWapień: " + Entity.calcium.ToString() + " kg\nŻelazo: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nTryb: " + Enum.GetName(typeof(Mode), currentMode) + "\nWybrany budynek: " + Entity.GetNameForType(selectedEnt.type) + "\nWytrzymałość: " + selectedEnt.health.ToString() + "\nPoziom: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        else
                            gameG.DrawString("\nPopulacja: " + Entity.population.ToString() + "\nPieniądze: " + Entity.cash.ToString() + " YEN\nBambus: " + Entity.bamboo.ToString() + " kg\nPiasek: " + Entity.sand.ToString() + " kg\nWapień: " + Entity.calcium.ToString() + " kg\nŻelazo: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nTryb: " + Enum.GetName(typeof(Mode), currentMode) + "\n", new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        if (selectedEnt != null && (useEyeTracker && !lockCursor))
                            gameG.DrawString("Wybrany budynek: " + Entity.GetNameForType(selectedEnt.type) + "\nWytrzymałość: " + selectedEnt.health.ToString() + "\nPoziom: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(lastPosX, lastPosY));
                        else
                        {
                            lastPosX = __gameCursorX;
                            lastPosY = __gameCursorY;
                        }
                    }
                    else if (Program.lang == Localization.Language.zhongwen) 
                    {
                        if (Entity.population == 0)
                        {
                            gameG.DrawString("\u6309ESC\u91cd\u65b0\u5f00\u59cb", new Font(FontFamily.GenericMonospace,
                 16.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 180));
                            gameG.DrawString("\u6e38\u620f\n\u7ed3\u675f", new Font(FontFamily.GenericMonospace,
                 72.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(300, 200));
                        }
                        gameG.DrawString("\u5f53\u524d\u623f\u5b50: " + Entity.GetNameForType(currentSelection), new Font(FontFamily.GenericMonospace,
                12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(110, 0));
                        if (selectedEnt != null && (!useEyeTracker || lockCursor))
                            gameG.DrawString("\n\u4eba\u53e3: " + Entity.population.ToString() + "\n\u94B1: " + Entity.cash.ToString() + " YEN\n\u7AF9\u5B50: " + Entity.bamboo.ToString() + " kg\n\u6C99: " + Entity.sand.ToString() + " kg\n\u77F3\u7070\u5CA9: " + Entity.calcium.ToString() + " kg\n\u94C1: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\u6A21\u5F0F: " + Enum.GetName(typeof(Mode), currentMode) + "\n\u5165\u9009\u623f\u5b50: " + Entity.GetNameForType(selectedEnt.type) + "\n\u5065\u5EB7: " + selectedEnt.health.ToString() + "\n\u7EA7\u522B: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        else
                            gameG.DrawString("\n\u4eba\u53e3: " + Entity.population.ToString() + "\n\u94B1: " + Entity.cash.ToString() + " YEN\n\u7AF9\u5B50: " + Entity.bamboo.ToString() + " kg\n\u6C99: " + Entity.sand.ToString() + " kg\n\u77F3\u7070\u5CA9: " + Entity.calcium.ToString() + " kg\n\u94C1: " + Entity.iron + " kg\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\u6A21\u5F0F: " + Enum.GetName(typeof(Mode), currentMode) + "\n", new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(0, 0));
                        if (selectedEnt != null && (useEyeTracker && !lockCursor))
                            gameG.DrawString("\u5165\u9009\u623f\u5b50: " + Entity.GetNameForType(selectedEnt.type) + "\n\u5065\u5EB7: " + selectedEnt.health.ToString() + "\n\u7EA7\u522B: " + (1 + selectedEnt.level).ToString(), new Font(FontFamily.GenericMonospace,
                   12.0F, FontStyle.Bold), new SolidBrush(Color.White), new Point(lastPosX, lastPosY));
                        else
                        {
                            lastPosX = __gameCursorX;
                            lastPosY = __gameCursorY;
                        }
                    }

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

            gameLogo = new Bitmap(FileSystem.GetBitmapFromFile("island_of_waves"));
            opLabel = new Bitmap(FileSystem.GetBitmapFromFile("op"));

            menuBackground = FileSystem.GetAnimSpriteFromFiles("Background_Animation 1", 4);

            bWaterWaves = FileSystem.GetAnimSpriteFromFiles("game/wave_Animation 1", 5);
            bFireWaves = FileSystem.GetAnimSpriteFromFiles("game/fire Animation", 4);
            bTornado = FileSystem.GetAnimSpriteFromFiles("game/wind_Animation 1", 5);
            bEarthquake = FileSystem.GetAnimSpriteFromFiles("game/earth_Animation 1", 5);

            bSandMaker = FileSystem.GetAnimSpriteFromFiles("game/jp_snd_mkr", 4);
            bBambooFarmer = FileSystem.GetAnimSpriteFromFiles("game/jp_bmb_frm", 4);
            bCalciumMine = FileSystem.GetAnimSpriteFromFiles("game/jp_clc_min", 4);
            bIronForge = FileSystem.GetAnimSpriteFromFiles("game/jp_irn_frg", 4);
            bUpgrade = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_rsc_flr"));

            bHouseLarge = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_lg"));
            bHouseMedium = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_md"));
            bHouseSmall = new Bitmap(FileSystem.GetBitmapFromFile("game/jp_house_sm"));

            bGrassBG = new Bitmap(FileSystem.GetBitmapFromFile("game/map_empty"));
            bBambooWall = new Bitmap(FileSystem.GetBitmapFromFile("game/BambooWall"));
            bBambooWallFG = new Bitmap(FileSystem.GetBitmapFromFile("game/BambooWall_fg"));
        }
        public enum Disaster
        {
            None,
            Water,
            Fire,
            Wind,
            Earthquake
        }
        public int nextDisaster = 65;
        public Disaster currentDisaster = Disaster.None;
        public Disaster nextDis;
        public bool playedStinger = false;
        public bool assignedNextDis = false;
        private void waveTimer_OnTick(object sender, EventArgs e)
        {
            fps = fc % prevfc;
            prevfc = fc;
            counter++;
            Random r = new Random();
            if (!assignedNextDis)
            {
                nextDis = (Disaster)r.Next((int)Disaster.None + 1, Enum.GetValues(typeof(Disaster)).Length);
                assignedNextDis = true;
            }
            if(nextDisaster - counter <= 8 && nextDis == Disaster.Water && !playedStinger)
            {
                Sound.PlayASound("water_wave_sound_1");
                playedStinger = true;
            }
            if (nextDisaster - counter <= 8 && nextDis == Disaster.Wind && !playedStinger)
            {
                Sound.PlayASound("wind_wave_sound_1");
                playedStinger = true;
            }
            if (counter % nextDisaster == 0)
            {
                counter = 0;
                nextDisaster = r.Next(10,25);
                currentDisaster = nextDis;
                assignedNextDis = false;
                playedStinger = false;
                for(int i = 0; i < Entity.entList.Count; i++)
                {
                    Entity ent = Entity.entList[i];
                    ent.health -= r.Next(100);
                    if (ent.health < 0)
                    {
                        Entity.entList.Remove(ent);
                        Sound.PlayASound("break_sound_1");
                    }
                }
            }
            int req_population = 0;
            foreach (Entity ent in Entity.entList)
            {
                req_population += ent.require_population;
            }
            foreach (Entity ent in Entity.entList)
            {
                Entity.add_cash -= ent.cost;
                ent.cost = 0;
                Entity.add_bamboo -= ent.bamboo_cost;
                ent.bamboo_cost = 0;
                Entity.add_iron -= ent.iron_cost;
                ent.iron_cost = 0;
                Entity.add_sand -= ent.sand_cost;
                ent.sand_cost = 0;
                Entity.add_calcium -= ent.calcium_cost;
                ent.calcium_cost = 0;
                switch (ent.type)
                {
                    case EntType.jp_house_lg:
                        Entity.add_cash += (int)((50 + 5 * ent.level));
                        break;
                    case EntType.jp_house_md:
                        Entity.add_cash += (int)((20 + 2 * ent.level));
                        break;
                    case EntType.jp_house_sm:
                        Entity.add_cash += (int)((10 + 1 * ent.level));
                        break;

                    case EntType.jp_bmb_frm:
                        Entity.add_bamboo += (int)((25 + 7 * ent.level) * (req_population > Entity.population ? (float)Entity.population / (float)req_population : 1));
                        break;
                    case EntType.jp_snd_mkr:
                        Entity.add_sand += (int)((30 + 5 * ent.level) * (req_population > Entity.population ? (float)Entity.population / (float)req_population : 1));
                        break;
                    case EntType.jp_irn_frg:
                        Entity.add_iron += (int)((15 + 3 * ent.level) * (req_population > Entity.population ? (float)Entity.population / (float)req_population : 1));
                        break;
                    case EntType.jp_clc_min:
                        Entity.add_calcium += (int)((15 + 3 * ent.level) * (req_population > Entity.population ? (float)Entity.population / (float)req_population : 1));
                        break;
                }
            }
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
           
            if (currentDisaster != Disaster.None)
            {
                switch (currentDisaster)
                {
                    case Disaster.Earthquake:
                        if(bEarthquake.currentFrame == 4)
                        {
                            currentDisaster = Disaster.None;
                        }
                        bEarthquake.AdvanceFrame();
                        break;
                    case Disaster.Fire:
                        if (bFireWaves.currentFrame == 3)
                        {
                            currentDisaster = Disaster.None;
                        }
                        bFireWaves.AdvanceFrame();
                        break;
                    case Disaster.Water:
                        if (bWaterWaves.currentFrame == 4)
                        {
                            currentDisaster = Disaster.None;
                        }
                        bWaterWaves.AdvanceFrame();
                        break;
                    case Disaster.Wind:
                        if (bTornado.currentFrame == 4)
                        {
                            currentDisaster = Disaster.None;
                        }
                        bTornado.AdvanceFrame();
                        break;
                }
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            fc++;
            f = fc.ToString();
            this.Refresh();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            gameB = new Bitmap(800, 600);
            this.CenterToScreen();
            this.Refresh();
        }
    }
}