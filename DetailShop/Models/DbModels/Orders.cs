using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Orders
    {
        [Key] public int ID_Order { get; set; }
        public int ID_User { get; set; }
        public decimal Result { get; set; }
    }
}
