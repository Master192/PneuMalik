using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PneuMalik.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.Cathegory> Cathegories { get; set; }

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.Product> Products { get; set; }

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.Text> Texts { get; set; }

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.Manufacturer> Manufacturers { get; set; }

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.Season> Seasons { get; set; }

        public System.Data.Entity.DbSet<PneuMalik.Models.Dto.PriceObject> Prices { get; set; }
    }
}