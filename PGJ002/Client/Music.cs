using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;


namespace PGJ002
{
    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class LoopStream : WaveStream
    {
        WaveStream sourceStream;

        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</param>
        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>
        public bool EnableLooping { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>
        public override WaveFormat WaveFormat
        {
            get { return sourceStream.WaveFormat; }
        }

        /// <summary>
        /// LoopStream simply returns
        /// </summary>
        public override long Length
        {
            get { return sourceStream.Length; }
        }

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>
        public override long Position
        {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                    {
                        // something wrong with the source stream
                        break;
                    }
                    // loop
                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
    class Music
    {
        static List<SndH> snds = new List<SndH>();
        public static void SetMusic(string name)
        {
            foreach(var snde in snds)
            {
                snde.w.Stop();
                snde.r2.CurrentTime = new TimeSpan(0);
            }
            string p = FileSystem.GetMusicPath(name);
            if (snds.Find(x => x.name == p) == null)
            {
                WaveFileReader snd;
                LoopStream sndl;
                WaveOutEvent wve = new WaveOutEvent();
                snd = new WaveFileReader(p);
                sndl = new LoopStream(snd);
                wve.Init(sndl);
                wve.PlaybackStopped += Wve_PlaybackStopped;
                snds.Add(new SndH(p, sndl, wve));
            }
            if (snds.Find(x => x.name == p) != null)
            {
                snds.Find(x => x.name == p).r2.CurrentTime = new TimeSpan(0);
                snds.Find(x => x.name == p).w.Play();
            }
        }

        private static void Wve_PlaybackStopped(object sender, StoppedEventArgs e)
        {
        }
    }
}
