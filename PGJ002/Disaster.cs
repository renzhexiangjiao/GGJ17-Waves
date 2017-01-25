using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGJ002
{
    public class Disaster
    {
        public enum Type
        {
            None,
            Water,
            Fire,
            Wind,
            Earthquake
        }
        public static Type currentDisaster = Type.None;
        public static Type nextDisaster;
        public static int nextDisasterIn = 20;
        public static void Tick(object sender, EventArgs e)
        {
            nextDisasterIn--;
            Random r = new Random();
            if (nextDisasterIn == 19)
            {
                currentDisaster = Type.None;
                nextDisaster = (Type)r.Next((int)Type.None + 1, Enum.GetValues(typeof(Type)).Length);
            }
            if (nextDisasterIn == 8)
            {
                switch (nextDisaster)
                {
                    case Type.Water:
                        Sound.PlayASound("water_wave_sound_1");
                        break;
                    case Type.Wind:
                        Sound.PlayASound("wind_wave_sound_1");
                        break;
                    case Type.Fire:
                        break;
                    case Type.Earthquake:
                        break;
                }
            }
            if (nextDisasterIn == 0)
            {
                currentDisaster = nextDisaster;
                for (int i = 0; i < Entity.entList.Count; i++)
                {
                    Entity ent = Entity.entList[i];
                    ent.health -= r.Next(25, 75);
                    if (ent.health < 0)
                    {
                        Entity.entList.Remove(ent);
                        Sound.PlayASound("break_sound_1");
                    }
                }
                nextDisasterIn = 20;
            }
        }
    }
}
