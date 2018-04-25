
namespace FootballBetting.Client
{
    using FootballBetting.Data.Database;

    public class Program
    {
        public static void Main()
        {
            FootballBettingDbContext db = new FootballBettingDbContext();

            using (db)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
