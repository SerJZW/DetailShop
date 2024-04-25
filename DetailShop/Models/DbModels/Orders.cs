using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailShop.Models.DbModels
{
    public class Orders
    {
        [Key] public int ID_Order { get; set; }
        public int ID_User { get; set; }
        public decimal Result { get; set; }

        [ForeignKey("OrderID")]
        public List<Order_Item> Items { get; set; }

        public Orders()
        {
            Items = new List<Order_Item>();
        }
    }
}
