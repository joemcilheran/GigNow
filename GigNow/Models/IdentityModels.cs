using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigNow.Models
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

        public System.Data.Entity.DbSet<GigNow.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Artist> Artists { get; set; }



        public System.Data.Entity.DbSet<GigNow.Models.Photo> Photos { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Track> Tracks { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Video> Videos { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.ArtistRelationship> ArtistRelationships { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Listener> Listeners { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Zipcode> Zipcodes { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Gig> Gigs { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Venue> Venues { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.GigRelationship> GigRelationships { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.Slot> Slots { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.VenueRelationship> VenueRelationships { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.VenueViewModelVM> VenueViewModelVMs { get; set; }

        public System.Data.Entity.DbSet<GigNow.Models.ArtistViewModelVM> ArtistViewModelVMs { get; set; }
    }
}