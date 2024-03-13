using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Order_Item
    {
        [Key] public int ID_OrderItem { get; set; }
        public int ID_Order { get; set; }
        public int ID_Component { get; set; }
        public int Quanity { get; set; }
    }
}
