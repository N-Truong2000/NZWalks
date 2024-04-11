using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext() { }
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {


        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dificulties = new List<Difficulty>() {
                new Difficulty()
                {
                    Id=Guid.Parse("c75f1fbc-e53b-4684-a6e4-1b418be72111"),
                    Name="Easy"
                },
                  new Difficulty()
                {
                    Id=Guid.Parse("aac0b969-806a-426d-aeba-095be6b354c0"),
                    Name="Medium"
                },
                  new Difficulty()
                {
                    Id=Guid.Parse("947bed47-1b7f-4ed7-89d3-caff90030b3a"),
                    Name="Hard"
                },

            };

            modelBuilder.Entity<Difficulty>().HasData(dificulties);

            var regios = new List<Region>()
            {
                new Region()
                {
                    Id= Guid.Parse("9905f19a-dca5-40a4-8b4f-ffc2090f2976"),
                    Name="AuckLand",
                    Code="5899",
                    RegionImageUrl="https://www.google.com/url?sa=i&url=https%3A%2F%2Fsnapshot.canon-asia.com%2Fvn%2Farticle%2Fviet%2Flandscape-photography-quick-tips-for-stunning-deep-focused-images&psig=AOvVaw10bfNlfnVznIXoqSjdr7n6&ust=1712307601380000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCMDj-ISZqIUDFQAAAAAdAAAAABAE"
                },
                new Region()
                {
                    Id= Guid.Parse("1ef6a9d2-48a1-4528-a1ff-e41dfe38c06b"),
                    Name="NelSon",
                    Code="5990",
                    RegionImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMhaMUyMmwZFNmsB3fNsXJARE6p8RHXsYCfSM_4qAGHOgn0dhNin0siPsuQpJF66SqrQo&usqp=CAU"
                },
                new Region()
                {
                    Id= Guid.Parse("b50812fa-ee88-4d12-af90-85e597e45251"),
                    Name="SoutnLand",
                    Code="6983",
                    RegionImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRejEC0q_n6SqMGbJMC5Q8H1bgsqN0YMln_amAkMSOmHw&s"
                },
                new Region()
                {
                    Id= Guid.Parse("e12cb6cb-1560-41ad-a653-34f5d92eca7e"),
                    Name="AN Do",
                    Code="6214",
                    RegionImageUrl=null
                },
            };
            modelBuilder.Entity<Region>().HasData(regios);
        }
    }
}
