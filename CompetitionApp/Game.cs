using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Services;
using Services.Model;

namespace CompetitionApp
{
    public class Game<T, U> where T : Battleable
                              where U : Battleable
    {
        Player player;
        Enemy enemy;
        Validator validator = new Validator();
        ConsoleColor playercolor;
        ConsoleColor enemycolor;
        public List<Item> items;
        ItemFactory factory = new ItemFactory();
        private Service service = new Service();

        public Game()
        {
          
            Console.ResetColor();
            items = new List<Item>();
            addItems();

            player = new Player() {HP = 20, atk = 5, def = 4, Inventory = new List<Item>()};
            enemy = new Enemy() { name = "Enemy", HP = 20, atk = 5, def = 4, Inventory = new List<Item>()};
            SetPlayer();
            validator.Validate(player);

            if(!player.isValid)
            {
                Console.WriteLine("Invalid player, input data again:");
                SetPlayer();
            }
            PickItem(items);
            PickItem2(items);
         

            player.isAlive = true;
            enemy.isAlive = true;

            var counter = 0;
            while (counter < 3)
            {
                foreach (Item item in items)
                {
                    enemy.Inventory.Add(items[counter]);
                    counter++;
                }
            }

            player.InitializeStats(player.Inventory);
            enemy.InitializeStats(enemy.Inventory);
            Compete(player, enemy);
            Console.ResetColor();
            End(player, enemy);
        }

        public void SetPlayer()
        {
            SetName();
            PickColor();
        }
     public void addItems()
        {
            items.Add(ItemFactory.CreateItem("Sword", "Decent sword.", 3, null, null));
            items.Add(ItemFactory.CreateItem("Shield", "Decent shield.", null, 3, null));
            items.Add(ItemFactory.CreateItem("Chestplate", "Protects your heart.", null, null, 3));
            items.Add(ItemFactory.CreateItem("Cursed Spear", "No reward without risk.", 7, -3, null));
            items.Add(ItemFactory.CreatePoison("Poison Bow", "Large bow with poisoned arrows.", null, null, null));   
        }
  
        public void SetName()
        {
            Console.WriteLine("What is your name?");
            player.name = Console.ReadLine();
         
        }

        public void PickColor()
        {
            Console.WriteLine("What is your uniform color? \n1. Red  2. Blue  3. Green  4. Yellow");
            string input = Console.ReadLine();
            switch (input)
            {
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    PickColor();
                    break;
                case "1":
                    playercolor = ConsoleColor.Red;
                    break;
                case "2":
                    playercolor = ConsoleColor.Blue;
                    break;
                case "3":
                    playercolor = ConsoleColor.Green;
                    break;
                case "4":
                    playercolor = ConsoleColor.Yellow;
                    break;
            }
                    var enemyColor = new Random().Next(1, 4);
                 while (enemyColor.ToString() == input)
              {
                enemyColor = new Random().Next(1, 4);
             }
            switch (enemyColor)
                    { 
                      case 1:
                        enemycolor = ConsoleColor.Red;
                        break;
                    case 2:
                        enemycolor = ConsoleColor.Blue;
                        break;
                    case 3:
                        enemycolor = ConsoleColor.Green;
                        break;
                    case 4:
                        enemycolor = ConsoleColor.Yellow;
                        break;
                    default:
                        enemycolor = ConsoleColor.Cyan;
                        break;
                    }
           

        }

            public void PickItem(List<Item> items)
            {
            var counter = 1;
            Console.WriteLine($"Choose an item:");
            foreach (Item item in items)
            {
                Console.WriteLine(counter);
                item.Info();
                counter++;
              
            }
            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    player.Inventory.Add(items[0]);
                    items.Remove(items[0]);
                    break;
                case "2":
                    player.Inventory.Add(items[1]);
                  items.Remove(items[1]);
                    break;
                case "3":
                    player.Inventory.Add(items[2]);
                    items.Remove(items[2]);
                    break;
                case "4":
                    player.Inventory.Add(items[3]);
                    items.Remove(items[3]);
                    break;
                case "5":
                    player.Inventory.Add(items[4]);
                    items.Remove(items[4]);
                    break;
                default:
                    player.Inventory.Add(items[new Random().Next(0, 3)]);
                    break;
            }

            }

            public void PickItem2(List<Item> items)
            {
            var counter = 1;
            Console.WriteLine($"Choose another item:");
            foreach (Item item in items)
            {
                Console.WriteLine(counter);
                item.Info();
                counter++;

            }
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player.Inventory.Add(items[0]);
                    items.Remove(items[0]);
                    break;
                case "2":
                    player.Inventory.Add(items[1]);
                    items.Remove(items[1]);
                    break;
                case "3":
                    player.Inventory.Add(items[2]);
                    items.Remove(items[2]);
                    break;
                case "4":
                    player.Inventory.Add(items[3]);
                    items.Remove(items[3]);
                    break;
            }
        }

            public void Compete(Battleable player, Battleable enemy)
            {
                while (player.HP > 0 && enemy.HP > 0)
              {
                if(player.isAlive == false || enemy.isAlive == false)
                {
                    break;
                }    

                Console.ForegroundColor = playercolor;
                    Battle(player, enemy);
                    if (player.HP <= 0)
                {
                    player.isAlive = false;
                    Console.ResetColor();
                    Console.WriteLine("You have been defeated!");
                        break;
                }

                Console.ForegroundColor = enemycolor;
                    Battle(enemy, player);
                    if (enemy.HP <= 0)
                {
                    enemy.isAlive = false;
                    Console.ResetColor();
                    Console.WriteLine("You have defeated the enemy!");
                        break;
                    }
              }
            }

        public void Battle(Battleable attacker, Battleable defender)
        {
            var damage = attacker.atk + new Random().Next(1, 3) - defender.def;

            if (damage <= 0)
            {
                damage = 1;
            }

            if (attacker.isPoison)
            {
                PoisonWeapon poison = attacker.Inventory.Find(i => i is PoisonWeapon) as PoisonWeapon;    
                poison.Attach(defender);
                poison.Damage();
            }

            defender.HP -= damage;

            Console.WriteLine($"{attacker.name} attacks {defender.name}!");
            Console.WriteLine($"{attacker.name} dealt {damage}!");

            if (defender.HP <= 0)
            {
                defender.isAlive = false;
                Console.WriteLine($"{defender.name} has lost!");
                
            }
        }
        private void End(Player player, Enemy enemy)
        {
            Console.WriteLine("Your inventory contained:");
            foreach (Item item in player.Inventory)
            {

                item.Info();
            }
            Console.WriteLine("The enemy's inventory contained:");
            foreach(Item item in enemy.Inventory)
            {
                item.Info();
            }

            RecordModel record = new RecordModel();
            record.playerName = player.name;
            record.atk = player.atk;
            record.hp = player.HP;

            service.InsertRecord(record);
            service.APIInsertRecord(record);

            Console.WriteLine("This is the end of the game! Press any key to start again.");
            Console.ReadKey();
            Console.Clear();
            new Game<Battleable, Battleable>();

        }

    }

   
    } 

