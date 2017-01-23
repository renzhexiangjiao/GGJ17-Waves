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
        public static int population
        {
            get
            {
                int r = 0;
                foreach (Entity ent in Entity.entList)
                {
                    r += ent.adds_to_population + 0*ent.level;
                }
                return r;
            }
        }
        public static int add_cash = 0;
        public static int add_bamboo = 0;
        public static int add_calcium = 0;
        public static int add_iron = 0;
        public static int add_sand = 0;
        public static int cash
        {
            get
            {
                int r = 0;
                foreach(Entity ent in Entity.entList)
                {
                    r += ent.cost + 50*ent.level;
                }
                return add_cash+1000-r;
            }
        }
        public static int bamboo
        {
            get
            {
                int r = 0;
                foreach (Entity ent in Entity.entList)
                {
                    r += ent.bamboo_cost + 100 * ent.level;
                }
                return add_bamboo + 1250 - r;
            }
        }
        public static int sand
        {
            get
            {
                int r = 0;
                foreach (Entity ent in Entity.entList)
                {
                    r += ent.sand_cost + 100 * ent.level;
                }
                return add_sand + 1500 - r;
            }
        }
        public static int calcium
        {
            get
            {
                int r = 0;
                foreach (Entity ent in Entity.entList)
                {
                    r += ent.calcium_cost + 75 * ent.level;
                }
                return add_calcium + 500 - r;
            }
        }
        public static int iron
        {
            get
            {
                int r = 0;
                foreach (Entity ent in Entity.entList)
                {
                    r += ent.iron_cost + 40 * ent.level;
                }
                return add_iron + 500 - r;
            }
        }

        // ENT-SPECIFIC
        public EntType type;
        public int health;
        public int maxhealth;
        public int level = 0;
        public int PositionX = 0;
        public int PositionY = 0;
        public bool isAnimated = false;

        // COST
        public int cost = 0;
        public int bamboo_cost = 0;
        public int sand_cost = 0;
        public int calcium_cost = 0;
        public int iron_cost = 0;

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

        public static void ResetWorld()
        {
            MainForm.nextDisaster = 20;
            entList.Clear();
            add_bamboo = 0;
            add_calcium = 0;
            add_cash = 0;
            add_iron = 0;
            add_sand = 0;
            CreateEntity(EntType.jp_house_sm, 1, 2);
        }

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
                    maxhealth = 150;
                    require_population = 2;
                    break;
                case EntType.jp_house_sm:
                    health = 75;
                    maxhealth = 75;
                    adds_to_population = 2;
                    break;
                case EntType.jp_house_md:
                    health = 150;
                    maxhealth = 150;
                    adds_to_population = 4;
                    break;
                case EntType.jp_house_lg:
                    health = 200;
                    maxhealth = 200;
                    adds_to_population = 8;
                    break;
                default:
                    health = 100;
                    maxhealth = 100;
                    break;
            }

            switch (type)
            {
                case EntType.jp_bmb_frm:
                    adds_to_bamboo = 25;
                    cost = 200;
                    animSprite = MainForm.bBambooFarmer;
                    break;
                case EntType.jp_clc_min:
                    adds_to_calcium = 15;
                    cost = 200;
                    animSprite = MainForm.bCalciumMine;
                    break;
                case EntType.jp_irn_frg:
                    adds_to_sand = 15;
                    cost = 200;
                    animSprite = MainForm.bIronForge;
                    break;
                case EntType.jp_snd_mkr:
                    adds_to_sand = 30;
                    cost = 200;
                    animSprite = MainForm.bSandMaker;
                    break;
                case EntType.jp_house_lg:
                    bamboo_cost = 800;
                    iron_cost = 275;
                    calcium_cost = 180;
                    sand_cost = 90;
                    sprite = MainForm.bHouseLarge;
                    break;
                case EntType.jp_house_md:
                    bamboo_cost = 500;
                    iron_cost = 150;
                    calcium_cost = 120;
                    sand_cost = 40;
                    sprite = MainForm.bHouseMedium;
                    break;
                case EntType.jp_house_sm:
                    bamboo_cost = 300;
                    iron_cost = 80;
                    calcium_cost = 80;
                    sand_cost = 10;
                    sprite = MainForm.bHouseSmall;
                    break;
            }
        }
        public static Bitmap GetSpriteForType(EntType type)
        {
            Bitmap sprite = new Bitmap(1, 1);
            switch (type)
            {
                case EntType.jp_bmb_frm:
                    sprite = MainForm.bBambooFarmer.GetCurrentFrame();
                    break;
                case EntType.jp_clc_min:
                    sprite = MainForm.bCalciumMine.GetCurrentFrame();
                    break;
                case EntType.jp_irn_frg:
                    sprite = MainForm.bIronForge.GetCurrentFrame();
                    break;
                case EntType.jp_snd_mkr:
                    sprite = MainForm.bSandMaker.GetCurrentFrame();
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
            return sprite;
        }
        public static string GetNameForType(EntType type)
        {
            string n = "null";
            if (Program.lang == Localization.Language.polish)
            {
                switch (type)
                {
                    case EntType.jp_bmb_frm:
                        n = "Farma Bambusa";
                        break;
                    case EntType.jp_clc_min:
                        n = "Kopalnia Wapienia";
                        break;
                    case EntType.jp_irn_frg:
                        n = "Kuźnia Żelaza";
                        break;
                    case EntType.jp_snd_mkr:
                        n = "Wykop Piasku";
                        break;
                    case EntType.jp_house_lg:
                        n = "Dom (Duży)";
                        break;
                    case EntType.jp_house_md:
                        n = "Dom (Średni)";
                        break;
                    case EntType.jp_house_sm:
                        n = "Dom (Mały)";
                        break;
                }
            }
            else if (Program.lang == Localization.Language.zhongwen)  // WYMAGA TŁUMACZENIA
            {
                switch (type)
                {
                    case EntType.jp_bmb_frm:
                        n = "\u7530\u7AF9\u5B50";
                        break;
                    case EntType.jp_clc_min:
                        n = "\u77F3\u7070\u5CA9\u77FF";
                        break;
                    case EntType.jp_irn_frg:
                        n = "\u94C1\u953B";
                        break;
                    case EntType.jp_snd_mkr:
                        n = "\u6C99\u6316\u65B9";
                        break;
                    case EntType.jp_house_lg:
                        n = "\u623f\u5b50 (\u5927)";
                        break;
                    case EntType.jp_house_md:
                        n = "\u623f\u5b50 (\u4E2D\u578B)";
                        break;
                    case EntType.jp_house_sm:
                        n = "\u623f\u5b50 (\u5C0F)";
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case EntType.jp_bmb_frm:
                        n = "Bamboo Farm";
                        break;
                    case EntType.jp_clc_min:
                        n = "Limestone Mine";
                        break;
                    case EntType.jp_irn_frg:
                        n = "Iron Forge";
                        break;
                    case EntType.jp_snd_mkr:
                        n = "Sand Excavation";
                        break;
                    case EntType.jp_house_lg:
                        n = "House (Large)";
                        break;
                    case EntType.jp_house_md:
                        n = "House (Medium)";
                        break;
                    case EntType.jp_house_sm:
                        n = "House (Small)";
                        break;
                }
            }
            return n;
        }
    }
}
