using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGJ002
{
    public struct Cost
    {
        public int cash_cost, bamboo_cost, sand_cost, limestone_cost, iron_cost;

        public Cost(int cash_cost, int bamboo_cost, int sand_cost, int limestone_cost, int iron_cost)
        {
            this.cash_cost = cash_cost;
            this.bamboo_cost = bamboo_cost;
            this.sand_cost = sand_cost;
            this.limestone_cost = limestone_cost;
            this.iron_cost = iron_cost;
        }

        public Cost halved
        {
            get
            {
                Cost hcost = new Cost(cash_cost/2, bamboo_cost/2, sand_cost/2, limestone_cost/2, iron_cost/2);
                return hcost;
            }
        }
    }
    public class Resources
    {
        public static int __population;
        public static int __cash;
        public static int __bamboo;
        public static int __sand;
        public static int __limestone;
        public static int __iron;

        public static int init_bamboo, init_sand, init_limestone, init_iron;

        public static void CollectTaxes(int population)
        {
            __cash += population * 4;
        }

        public static void Pay(Cost cost)
        {
            if (!MainForm.GodModeOn)
            {
                __cash -= cost.cash_cost;
                __bamboo -= cost.bamboo_cost;
                __sand -= cost.sand_cost;
                __limestone -= cost.limestone_cost;
                __iron -= cost.iron_cost;
            }
        }

        public static void Reset()
        {
            __population = 0;
            __cash = 0;
            __bamboo = 1250;
            __sand = 500;
            __limestone = 750;
            __iron = 500;
        }

        public static int population
        {
            get
            {
                __population = 0;
                foreach(Entity ent in Entity.entList)
                {
                    __population += ent.adds_to_population * (ent.level + 2);
                }
                return __population;
            }
        }
        public static int cash
        {
            get
            {
                return __cash;
            }
        }
        public static int bamboo
        {
            get
            {
                return __bamboo;
            }
        }
        public static int sand
        {
            get
            {
                return __sand;
            }
        }
        public static int limestone
        {
            get
            {
                return __limestone;
            }
        }
        public static int iron
        {
            get
            {
                return __iron;
            }
        }
    }
}
