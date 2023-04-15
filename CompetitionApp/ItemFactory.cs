using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionApp
{
    public class ItemFactory
    {
     
        public static Item CreateItem(string name, string desc, int? atk, int? def, int? hp)
        {
            Item item = new Item();


            item.name = name;
            item.desc = desc;
            item.def = def;
            item.atk = atk;
            item.hp = hp;
            return item;
        }

        public static PoisonWeapon CreatePoison(string name, string desc, int? atk, int? def, int? hp)
        {
            PoisonWeapon item = new PoisonWeapon();


            item.name = name;
            item.desc = desc;
            item.def = def;
            item.atk = atk;
            item.hp = hp;
            return item;
        }

    }
}
