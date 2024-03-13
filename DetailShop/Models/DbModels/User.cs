using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class User
    {
        [Key] public int ID_User { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? Discount { get; set; }
        public int? ID_Account { get; set; }
    }
}
