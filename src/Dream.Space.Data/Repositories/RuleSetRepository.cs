﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dream.Space.Data.Entities.Strategies;
using Dream.Space.Models.Enums;

namespace Dream.Space.Data.Repositories
{
    public interface IRuleSetRepository
    {
        RuleSet Get(int id);
        RuleSet Add(RuleSet ruleSet);
        Task<RuleSet> GetAsync(int ruleSetId);
        Task CommitAsync();
        Task<List<RuleSet>> GetAsync(QuotePeriod period, bool includeDeleted);
        Task DeleteAsync(int id);
    }


    public class RuleSetRepository : DreamDbRepository<RuleSet>, IRuleSetRepository
    {
        public RuleSetRepository(DreamDbContext dbContext) : base(dbContext)
        {
        }

        public RuleSet Get(int id)
        {
            var record = Dbset.FirstOrDefault(r => r.RuleSetId == id);
            return record;
        }

        public async Task<List<RuleSet>> GetAsync(QuotePeriod period, bool includeDeleted)
        {
            var records = await Dbset.Where(r => !r.Deleted || includeDeleted && r.Period == period).OrderBy(r => r.Name).ToListAsync();
            return records;
        }


        public async Task<RuleSet> GetAsync(int ruleSetId)
        {
            var record = await Dbset.FirstOrDefaultAsync(r => r.RuleSetId == ruleSetId);
            return record;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await Dbset.FirstOrDefaultAsync(r => r.RuleSetId == id);
            if(record != null)
            {
                Dbset.Remove(record);
            }
        }
    }
}