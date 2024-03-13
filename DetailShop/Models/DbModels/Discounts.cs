using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailShop.Models.DbModels
{
    public class Discounts
    {
        [Key] public int ID_Discount { get; set; }
        public int? Discount { get; set; }
        public int? Total {  get; set; }
    }
}
