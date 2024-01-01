using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Services
{
    public interface IBaseService<T> where T : DomainObject
    {
        Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request);
        Task<SingleResultViewModel<T>> GetByIdAsync(int id);
        Task<SingleResultViewModel<T>> InsertAsync(T entity);
        Task<SingleResultViewModel<T>> UpdateAsync(T entity);
        Task<SingleResultViewModel<T>> DeleteAsync(int id);
    }
}
