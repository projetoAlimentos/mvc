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

      // if (!context.IdentityRole.Any()) {
      //   context.IdentityRole.Add(new IdentityRole { Name = "Administrador"});
      //   context.IdentityRole.Add(new IdentityRole { Name = "Professor"});
      //   context.IdentityRole.Add(new IdentityRole { Name = "Assistente"});
      //   context.IdentityRole.Add(new IdentityRole { Name = "Aluno"});
      // }
      context.SaveChanges();
    }
  }
}