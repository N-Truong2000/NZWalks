using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Collections.Immutable;
using System.Net.WebSockets;

namespace NZWalks.API.Services.Implements
{
    public class WalkService : IWalkService
    {
        private readonly NZWalksDbContext _dbContext;
        public WalkService(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            var existingDifficulty = await _dbContext.Difficulties.FindAsync(walk.DifficultyId);
            var existingRegion = await _dbContext.Regions.FindAsync(walk.RegionId);

            if (existingDifficulty == null || existingRegion == null)
            {
                throw new InvalidOperationException("Difficulty or Region not found");
            }

            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            #region nhieu cach
            //return await (from walk in _dbContext.Walks
            //              join region in _dbContext.Regions on walk.RegionId equals region.Id
            //              join difficulty in _dbContext.Difficulties on walk.DifficultyId equals difficulty.Id
            //              select new Walk
            //              {
            //                  Id = walk.Id,
            //                  Name = walk.Name,
            //                  Description = walk.Description,
            //                  LengthInKm = walk.LengthInKm,
            //                  WalkImageUrl = walk.WalkImageUrl,
            //                  Region = region,
            //                  Difficulty = difficulty,
            //              }).ToListAsync();

            //return _dbContext.Walks.Join(_dbContext.Regions, w => w.RegionId, r => r.Id, (walk, region) => new { Walk = walk, Region = region })
            //                       .Join(_dbContext.Difficulties, w => w.Walk.DifficultyId, r => r.Id, (w, difficulty) => new Walk
            //                       {
            //                           Id = w.Walk.Id,
            //                           Region = w.Region,
            //                           Name = w.Walk.Name,
            //                           Description = w.Walk.Description,
            //                           LengthInKm = w.Walk.LengthInKm,
            //                           WalkImageUrl = w.Walk.WalkImageUrl,
            //                           Difficulty = difficulty
            //                       });


            //return _dbContext.Walks.Include("Region").Include("Difficulty").ToImmutableList();
            #endregion
            var walks = _dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).AsQueryable();
            //filter
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterQuery = filterQuery.Trim();
                filterOn = filterOn.Trim().ToLower();

                walks = filterOn switch
                {
                    "name" => walks.Where(x => x.Name.Contains(filterQuery)),
                    "description" => walks.Where(x => x.Description.Contains(filterQuery)),
                    _ => walks

                };
            }
            //Sort
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var checkIsAscending = isAscending ?? true;
                sortBy = sortBy.Trim().ToLower();
                walks = sortBy.ToLower() switch
                {
                    "name" => checkIsAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name),
                    "length" => checkIsAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm),
                    _ => walks
                };
            }
            //Pagenaton
            var skipResult = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            var existingDifficulty = await _dbContext.Difficulties.FindAsync(walk.DifficultyId);
            var existingRegion = await _dbContext.Regions.FindAsync(walk.RegionId);

            if (existingDifficulty == null || existingRegion == null || existingWalk == null)
            {
                return null;
            }
            existingWalk.Id = id;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            _dbContext.Update(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
        public async Task<Walk> DeleteAsnyc(Guid id)
        {
            var existingWalk = await _dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
