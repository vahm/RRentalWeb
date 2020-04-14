using System.ComponentModel.DataAnnotations;

namespace RRental.Api.Models
{
    public enum Type
    {
         [Display(Name = "Heavy equipment")]
         HeavyEquipment = 0,
         [Display(Name = "Regular Equipment")]
         RegularEquipment = 1,
         [Display(Name = "Specialized Equipment")]
         SpecializedEquipment = 2
    }
}