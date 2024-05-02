using DetailShop.Models.DbModels;

namespace DetailShop.Models.Menu
{
    public class ListMenu
    {
        public List<ItemMenu> GuestHeaderMenu = new List<ItemMenu>()
        {
            new ItemMenu("Home","Index","DetailShop"),
            new ItemMenu("Home","Components", "Каталог"),
            new ItemMenu("Home", "Discount", "Скидки"),
            new ItemMenu("Home", "Providers", "Производители"),
            new ItemMenu("Authentication", "Enter", "Войти"),
        };

        public List<ItemMenu> UserHeaderMenu = new List<ItemMenu>()
        {
            new ItemMenu("Home","Index","DetailShop"),
            new ItemMenu("Home","Components", "Каталог"),
            new ItemMenu("Home", "Discount", "Скидки"),
            new ItemMenu("Home", "Providers", "Производители"),
            new ItemMenu("Authentication", "Exit", "Выйти"),
        };
        public List<ItemMenu> AdminHeaderMenu = new List<ItemMenu>()
        {
            new ItemMenu("Home","Index","DetailShop"),
            new ItemMenu("Home","Components", "Каталог"),
            new ItemMenu("Home", "Discount", "Скидки"),
            new ItemMenu("Home", "Providers", "Производители"),
            new ItemMenu("Authentication", "Exit", "Выйти"),
        };
        public List<ItemMenu> GuestFooterMenu = new List<ItemMenu>()
        {
            new ItemMenu("Main","Index","О нас"),
            new ItemMenu("Main", "Reviews", "Отзывы"),
        };
        public List<ItemMenu> UserFooterMenu = new List<ItemMenu>()
        {
            new ItemMenu("Main","Index","О нас"),
            new ItemMenu("Main", "Reviews", "Отзывы"),
            new ItemMenu("Main", "Order", "Заказ"),
        };
        public List<ItemMenu> AdminFooterMenu = new List<ItemMenu>()
        {
            new ItemMenu("Main","Index","О нас"),
            new ItemMenu("Main","Users", "Пользователи"),
            new ItemMenu("Main", "Reviews", "Отзывы"),
        };
    }
}
