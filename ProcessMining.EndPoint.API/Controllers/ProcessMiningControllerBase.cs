using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseModels;

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

        [HttpGet]
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _baseService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _baseService.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task InsertAsync(T entity)
        {
            await _baseService.InsertAsync(entity);
        }
        [HttpPut]
        public async Task UpdateAsync(T entity)
        {
            await _baseService.UpdateAsync(entity);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _baseService.DeleteAsync(id);
        }
    }
}
