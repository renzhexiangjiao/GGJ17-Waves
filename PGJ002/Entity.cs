using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PGJ002
{
    public enum EntType
    {
        // -- RESIDENTIAL
        jp_house_lg,  // Large House
        jp_house_md,  // Medium House
        jp_house_sm,  // Small House
        // -- RESOURCES
        jp_bmb_frm,   // Bamboo Farm
        jp_clc_min,   // Calcium Mine
        jp_irn_frg,   // Iron Forge
        jp_snd_mkr,   // Sand Maker

        ent_max
    };
    
    public class Entity
    {
        // GLOBAL
        public static List<Entity> entList = new List<Entity>();
        public static int population = 0;
        public static int cash = 1000;
        public static int bamboo = 1250;
        public static int sand = 1500;
        public static int calcium = 500;
        public static int iron = 500;

        // ENT-SPECIFIC
        public EntType type;
        public int health;
        public int level = 1;
        public int PositionX = 0;
        public int PositionY = 0;
        public bool isAnimated = false;

        public Bitmap sprite;
        public AnimatedSprite animSprite;

        // POPULATION
        public int require_population = 0;
        public int adds_to_population = 0;

        // RESOURCES
        public int adds_to_bamboo = 0;
        public int adds_to_calcium = 0;
        public int adds_to_sand = 0;
        public int adds_to_iron = 0;

        // Creates an ent and returns index
        public static int CreateEntity(EntType type, int x, int y)
        {
            Entity ent;
            ent = new Entity(type);
            ent.PositionX = x;
            ent.PositionY = y;
            entList.Add(ent);
            return entList.Count;
        }
        public Entity(EntType type)
        {
            this.type = type;
            switch (type)
            {
                case EntType.jp_bmb_frm:
                case EntType.jp_clc_min:
                case EntType.jp_irn_frg:
                case EntType.jp_snd_mkr:
                    isAnimated = true;
                    health = 150;
                    require_population = 2;
                    break;
                case EntType.jp_house_sm:
                    health = 75;
                    adds_to_population = 2;
                    break;
                case EntType.jp_house_md:
                    health = 150;
                    adds_to_population = 4;
                    break;
                case EntType.jp_house_lg:
                    health = 200;
                    adds_to_population = 8;
                    break;
                default:
                    health = 100;
                    break;
            }

            switch (type)
            {
                case EntType.jp_bmb_frm:
                    adds_to_bamboo = 25;
                    animSprite = MainForm.bBambooFarmer;
                    break;
                case EntType.jp_clc_min:
                    adds_to_calcium = 15;
                    animSprite = MainForm.bCalciumMine;
                    break;
                case EntType.jp_irn_frg:
                    adds_to_sand = 15;
                    animSprite = MainForm.bIronForge;
                    break;
                case EntType.jp_snd_mkr:
                    adds_to_sand = 30;
                    animSprite = MainForm.bSandMaker;
                    break;
                case EntType.jp_house_lg:
                    sprite = MainForm.bHouseLarge;
                    break;
                case EntType.jp_house_md:
                    sprite = MainForm.bHouseMedium;
                    break;
                case EntType.jp_house_sm:
                    sprite = MainForm.bHouseSmall;
                    break;
            }
        }
    }
}
