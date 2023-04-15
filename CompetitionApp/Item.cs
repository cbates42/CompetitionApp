using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionApp
{
    public class Item
    {
        public string name { get; set; } = "";
        public string desc { get; set; } = "";
        public int? atk { get; set; }
        public int? def{ get; set; }

        public int? hp { get; set; }
        public void Info()
        {
            Console.WriteLine($"{name}: {desc}");
           if(atk != null)
            {
                Console.WriteLine($"Attack: {atk}");
            }
            if (def != null)
            {
                Console.WriteLine($"Defense: {def}");
            }
        }
    }
}
