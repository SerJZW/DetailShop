using DetailShop.Models.DbModels;

namespace DetailShop.Models
{
    public class UserViewModel
    {
        public List<Account> Accounts { get; set; } // Список пользователей для отображения

        public string NameUser { get; set; } // Данные для добавления нового пользователя
        public int Role { get; set; } // Данные для добавления нового пользователя

        public UserViewModel()
        {
            Accounts = new List<Account>();
        }
    }
}
