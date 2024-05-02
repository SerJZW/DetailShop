using System.ComponentModel.DataAnnotations;

namespace DetailShop.Models.DbModels
{
    public class Reviews
    {
        [Key] public int ID_Review { get; set; }
        public int ID_Component { get; set; }
        public int ID_Account { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }

    }
}
