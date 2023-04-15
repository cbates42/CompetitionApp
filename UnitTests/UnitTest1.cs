using CompetitionApp;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void InventoryTest()
        {
            CompetitionApp.Player player = new CompetitionApp.Player();
            player.Inventory = new List<Item>();

            player.Inventory.Add(CompetitionApp.ItemFactory.CreateItem("Sword", "Decent sword.", 3, null, null));
            player.Inventory.Add(CompetitionApp.ItemFactory.CreateItem("Sword", "Decent sword.", 3, null, null));
            player.Inventory.Add(CompetitionApp.ItemFactory.CreateItem("Sword", "Decent sword.", 3, null, null));

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(player, new ValidationContext(player), validationResults, true);

            Assert.False(isValid);
        }

        [Fact]
        public void NameLengthTest()
        {
            CompetitionApp.Player player = new CompetitionApp.Player();
            player.name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(player, new ValidationContext(player), validationResults, true);

            Assert.False(isValid);
        }
    }
}