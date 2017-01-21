using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PGJ002
{
    public class AnimatedSprite
    {
        public string alias;
        public Bitmap[] frames;
        public int currentFrame;
        public void AdvanceFrame()
        {
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;
        }
        public Bitmap GetCurrentFrame()
        {
            return frames[currentFrame];
        }
    }
}
