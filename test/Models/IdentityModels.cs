using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace test.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestModels>()
            .HasMany(c => c.Questions).WithMany(i => i.Tests)
            .Map(t => t.MapLeftKey("TestID")
            .MapRightKey("QuestionID")
            .ToTable("TestQuestions"));

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<test.Models.RoleModels> IdentityRoles { get; set; }

        public System.Data.Entity.DbSet<test.Models.QuestionModel> QuestionModels { get; set; }

        public System.Data.Entity.DbSet<test.Models.CategoryModels> CategoryModels { get; set; }

        public System.Data.Entity.DbSet<test.Models.TestModels> TestModels { get; set; }

        //public System.Data.Entity.DbSet<test.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}