using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;

namespace ProcessMining.EndPoint.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProcessMiningControllerBase<T> : ControllerBase where T : DomainObject
    {
        private readonly IBaseService<T> _baseService;

        public ProcessMiningControllerBase(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        [HttpPost]
        public virtual async Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request)
        {
            return await _baseService.GetAllAsync(request);
        }
        [HttpGet("{id}")]
        public virtual async Task<SingleResultViewModel<T>> GetByIdAsync(int id)
        {
            return await _baseService.GetByIdAsync(id);
        }
        [HttpPost]
        public virtual async Task<SingleResultViewModel<T>> InsertAsync(T entity)
        {
            return await _baseService.InsertAsync(entity);
        }
        [HttpPut]
        public virtual async Task<SingleResultViewModel<T>> UpdateAsync(T entity)
        {
            return await _baseService.UpdateAsync(entity);
        }
        [HttpDelete("{id}")]
        public virtual async Task<SingleResultViewModel<T>> DeleteAsync(int id)
        {
            return await _baseService.DeleteAsync(id);
        }
    }
}
