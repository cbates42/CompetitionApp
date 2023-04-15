using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompetitionApp
{
    internal class Validator
    {
        public void Validate(Player player)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var validationContext = new ValidationContext(player, null, null);

            player.isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(player, validationContext, errors, true);

            foreach (var error in errors)
            {
                foreach (var mem in error.MemberNames)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: Reason:{error.ErrorMessage}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
