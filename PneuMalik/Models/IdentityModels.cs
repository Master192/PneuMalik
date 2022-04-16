using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 600;
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Dto.Disk> Disks { get; set; }

        public DbSet<Dto.Cathegory> Cathegories { get; set; }

        public DbSet<Dto.Product> Products { get; set; }

        public DbSet<Dto.ProductsAluDisc> ProductsAluDisc { get; set; }

        public DbSet<Dto.ProductsPbDisc> ProductsPbDisc { get; set; }

        public DbSet<Dto.ProductsTyre> ProductsTyres { get; set; }

        public DbSet<Dto.ProductParamModel> ParamModel { get; set; }

        public DbSet<Dto.ProductParamProfil> ParamProfil { get; set; }

        public DbSet<Dto.ProductParamRafek> ParamRafek { get; set; }

        public DbSet<Dto.ProductParamSirka> ParamSirka { get; set; }

        public DbSet<Dto.ProductParamZnacka> ParamZnacka { get; set; }

        public DbSet<Dto.ProductParamSi> ParamSi { get; set; }

        public DbSet<Dto.ProductParamLi> ParamLi { get; set; }

        public DbSet<Dto.Text> Texts { get; set; }

        public DbSet<Dto.Manufacturer> Manufacturers { get; set; }

        public DbSet<Dto.PriceObject> Prices { get; set; }

        public DbSet<Dto.VehicleType> VehicleTypes { get; set; }

        public DbSet<Dto.CartRow> CartRows { get; set; }

        public DbSet<Dto.Customer> Customers { get; set; }

        public DbSet<Dto.Order> Orders { get; set; }

        public DbSet<Dto.OrderItem> OrderItems { get; set; }

        public DbSet<PneuB2b.Response.SteelRim> SteelRims { get; set; }

        public DbSet<PneuB2b.Response.Tyre> Tyres { get; set; }

        public DbSet<PneuB2b.PriceInfo> PriceInfos { get; set; }

        public DbSet<Dto.DiskItemSale> DiskSales { get; set; }
        public DbSet<Dto.DiskStock> DiskStocks { get; set; }
    }
}