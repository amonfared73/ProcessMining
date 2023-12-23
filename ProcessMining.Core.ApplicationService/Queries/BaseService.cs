﻿using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Infra.EntityFramework.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Queries
{
    public class BaseService<T> : IBaseService<T> where T : DomainObject
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;
        public BaseService(IDbContextFactory<ProcessMiningDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var query = context.Set<T>();
                return await query.ToListAsync();
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                return entity;
            }
        }

        public virtual async Task InsertAsync(T entity)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var currentEntity = await context.Set<T>().Where(e => e.Id == entity.Id).FirstOrDefaultAsync();
                context.Entry(currentEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
        }
        public virtual async Task DeleteAsync(int id)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}