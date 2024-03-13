using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Type_Component
    {
        [Key] public int ID_Type { get; set; }
        public string? Title { get; set; }
    }
}
