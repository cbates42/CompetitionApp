using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompetitionApp
{
   public class Player : Battleable
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(10, ErrorMessage = "Name is longer than 10 characters")]
        public override string name { get; set; }

        [Range(1, 25, ErrorMessage = "Health must be between 1 and 25.")]
        public override int HP { get; set; } = 1;
        public override int atk { get; set; } = 1;

        public bool isValid;

        public ConsoleColor color;


    }
}
