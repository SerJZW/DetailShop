using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Provider
    {
        [Key] public int ID_Provider { get; set; }
        public string? Title { get; set; }
        public int ID_Type { get; set; }
        public string? Information { get; set; }
    }
}
