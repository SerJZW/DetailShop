using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailShop.Models.DbModels
{
    public class Account
    {

        [Key] public int ID_Account { get; set; }
        public int? ID_Role { get; set; }
        public  DateOnly? Last_Sign { get; set; }
        public string? Login {  get; set; }
        public string? Password { get; set; }

    }
}
