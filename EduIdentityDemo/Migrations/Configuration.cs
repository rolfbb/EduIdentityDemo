using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EduIdentityDemo.Migrations
{
    using EduIdentityDemo.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EduIdentityDemo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EduIdentityDemo.Models.ApplicationDbContext";
        }

        protected override void Seed(EduIdentityDemo.Models.ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //TODO: Man får göra allt manuellt, på liknande sätt som sker i startup, etc

            //ej:
            //db.Roles.Add, istället:

            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Admin", "Editor" };
            foreach (var roleName in roleNames)
            {
                if (db.Roles.Any(r => r.Name == roleName)) continue; //Man kan använda db.Roles direkt också...
                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }


            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var emails = new string[]
                {"user@lexicon.se", "editor@lexicon.se", "admin@lexicon.se", "dimitris@lexicon.se"};
            foreach (var email in emails)
            {
                if (db.Users.Any(u => u.UserName == email)) continue;
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = userManager.Create(user, "foobar");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }


            var adminUser = userManager.FindByName("admin@lexicon.se");
            userManager.AddToRole(adminUser.Id, "Admin");

            var editorUser = userManager.FindByName("editor@lexicon.se");
            userManager.AddToRole(editorUser.Id, "Editor");

            var dimitris = userManager.FindByName("dimitris@lexicon.se");
            userManager.AddToRoles(dimitris.Id, "Admin", "Editor");

        }
    }
}
