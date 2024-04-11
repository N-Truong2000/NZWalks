using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext()
        {

        }
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var writerRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";
            var roles = new List<IdentityRole>()
            {
               new IdentityRole()
               {
                   Id=readerRoleId,
                   Name="Reader",
                   NormalizedName="Reader".ToLower(),
                   ConcurrencyStamp=readerRoleId,
               },
                 new IdentityRole()
               {
                   Id=writerRoleId,
                   Name="Writer",
                   NormalizedName="Writer".ToLower(),
                   ConcurrencyStamp=writerRoleId,
               }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
