using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using ProcessMining.Infra.EntityFramework.DbContextes;
using ProcessMining.Infra.Tools.Extentions;
using ProcessMining.Core.Domain.Responses;

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
                var rawData = await context.Set<T>().ToListAsync();
                var filteredData = rawData.ApplyBaseReuest(request);
                var totalRecords = rawData.Count();
                return new PagedResultViewModel<T>()
                {
                    Data = filteredData,
                    Pagination = request.PaginationRequest.ToPagination(totalRecords)
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> GetByIdAsync(int id)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("Id: {0}, {1}", id.ToString(), entity.ToString()))
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> InsertAsync(T entity)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} inserted successfully", entity.ToString()))
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> UpdateAsync(T entity)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                var currentEntity = await context.Set<T>().Where(e => e.Id == entity.Id).FirstOrDefaultAsync();
                context.Entry(currentEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} updated successfully", entity.ToString()))
                };
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
