using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Exception found!\n" + ex.ToString());
                res = Instance.defaultTex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found!\n" + ex.ToString());
                res = Instance.defaultTex;
            }
            return res;
        }
    }
}
