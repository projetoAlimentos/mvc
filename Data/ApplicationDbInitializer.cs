using projeto.Models;
using System.Linq;

namespace projeto.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();

            //context.SaveChanges();
        }
    }
}