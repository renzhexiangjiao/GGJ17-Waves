using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace PGJ002
{
    class Sound
    {
        static List<SoundPlayer> snds = new List<SoundPlayer>();
        public static void PlayASound(string name)
        {
            string p = FileSystem.GetSoundPath(name);
            SoundPlayer snd;
            if (snds.Find(x => x.SoundLocation == p) == null)
            {
                if (p != "--[null]--")
                    snd = new SoundPlayer(p);
                else
                    snd = new SoundPlayer(PGJ002.Properties.Resources._defaultSound);
                snds.Add(snd);
            } else
            {
                snd = snds.Find(x => x.SoundLocation == p);
            }
            Task.Run(() =>
            {
                snd.PlaySync();
            });
        }
    }
}
