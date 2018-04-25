

namespace OneToManyRelation
{
    using System;
    using OneToManyRelation.Database;

    public class Program
    {
        public static void Main()
        {
            CompanyDbContext db = new CompanyDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
