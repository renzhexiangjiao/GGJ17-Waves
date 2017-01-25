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

        // ENT-SPECIFIC
        public EntType type;
        public int health;
        public int maxhealth;
        public int level = 0;
        public int PositionX = 0;
        public int PositionY = 0;
        public bool isAnimated = false;

        // COST
        public Cost cost = new Cost(0, 0, 0, 0, 0);


        public Bitmap sprite;
        public AnimatedSprite animSprite;

        // POPULATION
        public int adds_to_population = 0;

        // RESOURCES
        public int adds_to_bamboo = 0;
        public int adds_to_calcium = 0;
        public int adds_to_sand = 0;
        public int adds_to_iron = 0;

        public static void ResetWorld()
        {
            MainForm.nextDisaster = 20;
            Disaster.nextDisasterIn = 20;
            entList.Clear();
            Resources.Reset();
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

        public static int BuildEntity(EntType type, int x, int y)
        {
            Entity ent;
            ent = new Entity(type);
            ent.PositionX = x;
            ent.PositionY = y;
            if(Resources.Pay(ent.cost))
                entList.Add(ent);
            return entList.Count;
        }

        public static void RepairEntity(Entity ent)
        {
            if (Resources.Pay(new Cost(0, 60, 60, 60, 60)))
                ent.health += 20;
            ent.health = Math.Min(ent.health, ent.maxhealth);
        }

        public static void UpgradeEntity(Entity ent)
        {
            if (Resources.Pay(ent.cost.halved))
                ent.level++;
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
                    adds_to_population = 0;
                    break;
                case EntType.jp_house_sm:
                    health = 75;
                    maxhealth = 75;
                    adds_to_population = 1;
                    break;
                case EntType.jp_house_md:
                    health = 150;
                    maxhealth = 150;
                    adds_to_population = 2;
                    break;
                case EntType.jp_house_lg:
                    health = 200;
                    maxhealth = 200;
                    adds_to_population = 4;
                    break;
                default:
                    health = 100;
                    maxhealth = 100;
                    adds_to_population = 0;
                    break;
            }

            switch (type)
            {
                case EntType.jp_bmb_frm:
                    adds_to_bamboo = 25;
                    cost = new Cost(200, 0, 0, 0, 0);
                    animSprite = MainForm.bBambooFarmer;
                    break;
                case EntType.jp_clc_min:
                    adds_to_calcium = 15;
                    cost = new Cost(200, 0, 0, 0, 0);
                    animSprite = MainForm.bCalciumMine;
                    break;
                case EntType.jp_irn_frg:
                    adds_to_sand = 15;
                    cost = new Cost(200, 0, 0, 0, 0);
                    animSprite = MainForm.bIronForge;
                    break;
                case EntType.jp_snd_mkr:
                    adds_to_sand = 30;
                    cost = new Cost(200, 0, 0, 0, 0);
                    animSprite = MainForm.bSandMaker;
                    break;
                case EntType.jp_house_lg:
                    cost = new Cost(0, 800, 275, 180, 90);
                    sprite = MainForm.bHouseLarge;
                    break;
                case EntType.jp_house_md:
                    cost = new Cost(0, 500, 150, 120, 40);
                    sprite = MainForm.bHouseMedium;
                    break;
                case EntType.jp_house_sm:
                    cost = new Cost(0, 300, 8, 80, 10);
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
                        n = "\u7AF9\u5B50\u7530";
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
