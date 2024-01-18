using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domains;

namespace NZWalks.Data
{
    public class NZWalkDbContext : DbContext
    {
        public NZWalkDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("2593F882-F2C7-4CFE-9E96-856AAD7AF6FE"),
                    Name = "Eassy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f0167be5-5c26-4c60-8161-640031a0c998"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ff683615-a46f-437a-8b09-ab486c1408f5"),
                    Name = "Hard"
                },
            };

            //seed dificulties to DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Regions
            var regions = new List<Region>
            {
                new Region()
                {
                    Id = Guid.Parse("858e6ad5-3fd0-472a-8d2d-863e0dd4a1f8"),
                    Name = "Wellington",
                    Code = "WLT",
                    ImageURL = "Ase.jpg"
                },
                 new Region()
                {
                    Id = Guid.Parse("c560da99-b186-47ed-9ae3-668c11bbc64d"),
                    Name = "Bogura",
                    Code = "BG",
                    ImageURL = null
                },
                 new Region()
                {
                    Id = Guid.Parse("1111b3f9-378c-40b1-a18a-0dc79546bd8b"),
                    Name = "Bogura",
                    Code = "BG",
                    ImageURL = "Ase.jpg"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
