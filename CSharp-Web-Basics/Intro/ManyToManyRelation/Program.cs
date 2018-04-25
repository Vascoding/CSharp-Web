

namespace ManyToManyRelation
{
    using ManyToManyRelation.Database;

    public class Program
    {
        public static void Main()
        {
            UniDbContext db = new UniDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
