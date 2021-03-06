﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Dream.Space.Data.Entities.Layouts;
using System.Collections.Generic;
using System.Linq;
using Dream.Space.Models.Enums;
using Dream.Space.Models.Layouts;

namespace Dream.Space.Data.Repositories
{
    public interface IChartLayoutRepository
    {
        Task<IChartLayoutEntity> GetAsync(int id);
        Task<IList<IChartLayoutEntity>> GetForPeriodAsync(QuotePeriod period);
        Task<IChartLayoutEntity> GetDefaultAsync(QuotePeriod period);
        ChartLayout Add(ChartLayout chartLayout);
        void Commit();
    }


    public class ChartLayoutRepository : DreamDbRepository<ChartLayout>, IChartLayoutRepository
    {
        public ChartLayoutRepository(DreamDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IChartLayoutEntity> GetAsync(int id)
        {
            var record = await Dbset.FirstOrDefaultAsync(r => r.LayoutId == id);
            return record;
        }

        public async Task<IList<IChartLayoutEntity>> GetForPeriodAsync(QuotePeriod period)
        {
            //var records = await Dbset.Where(r => r.Period == period).ToListAsync();
            //return records.Select(r => r as IChartLayoutEntity).ToList();
            throw new NotImplementedException();
        }

        public async Task<IChartLayoutEntity> GetDefaultAsync(QuotePeriod period)
        {
            //var record = await Dbset.FirstOrDefaultAsync(r => r.Period == period && r.Default) ??
            //             await Dbset.FirstOrDefaultAsync(r => r.Period == period);

            //return record;
            throw new NotImplementedException();

        }
    }
}