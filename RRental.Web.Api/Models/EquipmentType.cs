using System.ComponentModel.DataAnnotations;

namespace RRental.Web.Api.Models
{
    public enum EquipmentType
    {
         [Display(Name = "Heavy equipment")]
         HeavyEquipment = 0,
         [Display(Name = "Regular Equipment")]
         RegularEquipment = 1,
         [Display(Name = "Specialized Equipment")]
         SpecializedEquipment = 2
    }
}