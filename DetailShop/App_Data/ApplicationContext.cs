using DetailShop.Models.DbModels;
using Microsoft.EntityFrameworkCore;
namespace DetailShop.App_Data
 
{
    public class ApplicationContext : DbContext
    {
        #region DbSets
        public DbSet<Account> Account { get; set; } = null!;
        public DbSet<Components> Component { get; set; } = null!;
        public DbSet<Discounts> Discount { get; set; } = null!;
        public DbSet<Order_Item> Order_Item { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<Provider> Provider { get; set; } = null!;
        public DbSet<Reviews> Reviews { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<Type_Component> Type_Component { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        #endregion
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
      : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
