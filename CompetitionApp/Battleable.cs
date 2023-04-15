using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompetitionApp
{
    public abstract class Battleable
    {
        public virtual string name { get; set; }
        public virtual int HP { get; set; }
        public virtual int atk { get; set; }
        public virtual int def { get; set; }

        public virtual ConsoleColor color { get; set;}
        public bool isAlive { get; set; }

        [List(MaxItems = 2, ErrorMessage = "Cannot have more than two items in the inventory!")]
        public virtual List<Item> Inventory { get; set; }

        public bool isPoison { get; set; } = false;
        public void InitializeStats(List<Item> items)
        {
           
            foreach (Item item in items)
            {
                if (item.atk != null)
                {
                    this.atk += item.atk ?? 0;
                }

                if (item.def != null)
                {
                    this.def += item.def ?? 0;
                }

                if (item.hp != null)
                {
                    this.HP += item.hp ?? 0;
                }

                if(items.Any(i => i is PoisonWeapon))
                {
                    this.isPoison = true;
                }
            }
        }

        public void Update(PoisonWeapon poisonWeapon)
        {
            this.HP -= poisonWeapon.DamageActual;

            if(HP <= 0)
            {
             isAlive = false;
             HP = 0;
            }

            else 
            {

             Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{this.name} took {poisonWeapon.DamageActual} poison damage!");
            }
        }
    }
}
