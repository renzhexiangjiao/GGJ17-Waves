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
        public int previousFrame = 0;
        public void AdvanceFrame()
        {
            previousFrame = currentFrame;
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
