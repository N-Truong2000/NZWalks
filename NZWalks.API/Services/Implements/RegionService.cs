using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Services;

namespace NZWalks.API.Services.Implements
{
    public class RegionService : IRegionService
    {

        private readonly NZWalksDbContext _dbContext;

        public RegionService(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion =await _dbContext.Regions.FindAsync(id); 
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            _dbContext.Update(existingRegion);
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.AsNoTracking().ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var existingRegin= await _dbContext.Regions.FindAsync(id);
            if (existingRegin == null)
            {
                return null;
            }
            _dbContext.Remove(existingRegin);
            await _dbContext.SaveChangesAsync();
            return existingRegin;
        }
    }
}
