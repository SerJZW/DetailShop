using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Components
    {
        [Key] public int ID_Component { get; set; }
        public string? Name { get; set; }
        public int ID_Type { get; set; }
        public int ID_Provider { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public string? Specifications { get; set; }
        public int Count { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
