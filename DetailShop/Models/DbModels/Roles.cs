using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Roles
    {
        [Key] public int ID_Role { get; set; }
        public string? Title { get; set; }
    }
}
