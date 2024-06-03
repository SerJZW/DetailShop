using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailShop.Models.DbModels
{
    public class Orders
    {
        [Key] public int ID_Order { get; set; }
        public int ID_Account { get; set; }
        public decimal Result { get; set; }
        public int Component_Id { get; set; }


        [ForeignKey("Component_Id")]
        public virtual Components Component { get; set; }
    }
}
