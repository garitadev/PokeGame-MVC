using Microsoft.EntityFrameworkCore;

namespace PokeGame_MVC.Database
{

    public partial class PokeGameContext : Database.PokeGame.Context.PokeGameContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-993BNM1;Initial Catalog=PokeGame;Persist Security Info=True;User ID=sa;Password=Sistemas;TrustServerCertificate=True");
            }
        }
    }
}
