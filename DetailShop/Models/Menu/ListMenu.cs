namespace DetailShop.Models.Menu
{
    public class ListMenu
    {
        public List<ItemMenu> HeaderMenu = new List<ItemMenu>()
        {
            new ItemMenu("Home","Index","DetailShop"),
            new ItemMenu("Home","Components", "Каталог"),
            new ItemMenu("Home", "Discount", "Скидки"),
            new ItemMenu("Home", "Providers", "Производители"),
        };
        public List<ItemMenu> FooterMenu = new List<ItemMenu>()
        {
            new ItemMenu("Main","Index","О нас"),
            new ItemMenu("Main","Users", "Пользователи"),
            new ItemMenu("Main", "Reviews", "Отзывы"),
            new ItemMenu("Main", "Order", "Заказ"),
        };
    }
}
