using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionApp
{
    public class PoisonWeapon : Item
    {

        private List<Battleable> fighters = new List<Battleable>();
        private int damageInt;
        private int minDamage;
        private int maxDamage;
        public int DamageActual;

        public PoisonWeapon() { }
   

        public void Notify()
        {
        
            if(damageInt > 0)
            {
                Console.WriteLine($"Poison stays for: {damageInt} turns.");

                foreach(Battleable f in fighters)
                {
                    DamageActual = new Random().Next(minDamage, maxDamage);
                    if(f.isAlive)
                    {
                        f.Update(this);
                    }
                    if (damageInt <= 0)
                    {
                        Remove(f);
                    }
                }
                damageInt--;
            }

   
        }

    

        public void Damage(int minDamage = 1, int maxDamage = 3, int damageInterval = 7)
        {
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            damageInt = damageInterval;

            Notify();
        }

        public void Attach(Battleable fighter)
        {
            fighters.Add(fighter);
        }

        public void Remove(Battleable fighter)
        {
            fighters.Remove(fighter);
        }
    }
}
