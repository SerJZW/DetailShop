using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailShop.Models.DbModels
{
    public class Orders
    {
        [Key] public int ID_Order { get; set; }
        public int ID_Account { get; set; }
        public decimal Result { get; set; }

    }
}
