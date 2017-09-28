using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;


namespace PGJ002
{
    class Fonts
    {
        public static FontFamily[] fontFamilies;
        static string[] fontfiles = Directory.GetFiles("fonts/", "*.ttf", SearchOption.AllDirectories);
        static PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public static void AddFonts()
        { 
            foreach(string s in fontfiles)
                privateFontCollection.AddFontFile(s);
            fontFamilies = privateFontCollection.Families;
        }
    }
}
