using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio;
using NAudio.Wave;

namespace PGJ002
{
    public class SndH
    {
        public string name;
        public WaveFileReader r;
        public WaveOutEvent w;
        public LoopStream r2;
        public SndH(string n, WaveFileReader r, WaveOutEvent w)
        {
            this.name = n;
            this.r = r;
            this.w = w;
        }
        public SndH(string n, LoopStream r2, WaveOutEvent w)
        {
            this.name = n;
            this.r2 = r2;
            this.w = w;
        }
    }
    class Sound
    {
        static List<SndH> snds = new List<SndH>();
        public static void PlayASound(string name)
        {
            string p = FileSystem.GetSoundPath(name);
            if (snds.Find(x => x.name == p) == null)
            {
                WaveFileReader snd;
                WaveOutEvent wve = new WaveOutEvent();
                snd = new WaveFileReader(p);
                wve.Init(snd);
                snds.Add(new SndH(p, snd, wve));
            }
            if (snds.Find(x => x.name == p) != null)
            {
                snds.Find(x => x.name == p).r.CurrentTime = new TimeSpan(0);
                snds.Find(x => x.name == p).w.Play();
            }
        }
    }
}
