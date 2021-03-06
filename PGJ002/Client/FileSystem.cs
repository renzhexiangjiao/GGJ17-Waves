﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PGJ002
{
    class FileSystem
    {
        public Bitmap defaultTex = new Bitmap(PGJ002.Properties.Resources._default);//new Bitmap(2, 2);
        public static FileSystem Instance;
        public static void Initialize()
        {
            Instance = new FileSystem();
            Instance.defaultTex.SetPixel(0, 0, Color.Purple);
            Instance.defaultTex.SetPixel(1, 0, Color.CadetBlue);
            Instance.defaultTex.SetPixel(0, 1, Color.CadetBlue);
            Instance.defaultTex.SetPixel(1, 1, Color.Purple);
        }
        public static Bitmap GetBitmapFromFile(string filename)
        {
            Bitmap res;
            try
            {
                res = new Bitmap("sprites/" + filename + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found!\n" + filename + "\n" + ex.ToString());
                res = Instance.defaultTex;
            }
            return res;
        }
        public static Bitmap GetAnimFrameFromFile(string filename)
        {
            Bitmap res;
            try
            {
                res = new Bitmap("anim/" + filename + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found!\n" + filename + "\n" + ex.ToString());
                res = Instance.defaultTex;
            }
            return res;
        }
        public static Bitmap GetLocalizedBitmapFromFile(string filename)
        {
            Bitmap res;
            try
            {
                res = new Bitmap("sprites/" + filename + "_"+ Enum.GetName(typeof(Localization.Language), Program.lang) +".png");
            }
            catch
            {
                try
                {
                    res = new Bitmap("sprites/" + filename + ".png");
                    return res;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Exception found!\n"+filename+"\n" + ex2.ToString());
                    res = Instance.defaultTex;
                    return res;
                }
            }
            return res;
        }
        public static string GetSoundPath(string filename)
        {
            string res;
            if(File.Exists("sounds/"+filename+".wav"))
            {
                res = "sounds/" + filename + ".wav";
            } else
            {
                res = "--[null]--";
            }
            return res;
        }
        public static string GetMusicPath(string filename)
        {
            string res;
            if (File.Exists("music/" + filename + ".wav"))
            {
                res = "music/" + filename + ".wav";
                res = Path.GetFullPath(res);
            }
            else
            {
                res = "--[null]--";
            }
            return res;
        }
        public static AnimatedSprite GetAnimSpriteFromFiles(string name, int frameCount)
        {
            AnimatedSprite res = new AnimatedSprite();
            res.alias = name;
            res.frames = new Bitmap[frameCount];
            for(int i = 0; i < frameCount; i++)
            {
                res.frames[i] = GetAnimFrameFromFile(name + "_" + i.ToString());
            }
            return res;
        }
    }
}
