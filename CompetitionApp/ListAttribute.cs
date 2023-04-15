using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompetitionApp
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ListAttribute : ValidationAttribute
    {

        public int MaxItems { get; set; } = 2;
  

        public override bool IsValid(object value)
        {
            List<Item> items = value as List<Item>;
            if(items == null)
            {
                return false;
            }
       
            if(items.OfType<object>().Count() > MaxItems)
            {
                return false;
            }

            return true;
        }
    }
}
