using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using ProcessMining.Infra.EntityFramework.DbContextes;
using ProcessMining.Infra.Tools.Extentions;

namespace ProcessMining.Core.ApplicationService.Queries
{
    public class BaseService<T> : IBaseService<T> where T : DomainObject
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;
        public BaseService(IDbContextFactory<ProcessMiningDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public virtual async Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var data = await context.Set<T>().ToListAsync();
                var totalRecords = data.Count();
                return new PagedResultViewModel<T>()
                {
                    Data = data.ApplySearchTerm(request.SearchTermRequest).ApplyPagination(request.PaginationRequest),
                    Pagination = request.PaginationRequest.ToPagination(totalRecords)
                };
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
