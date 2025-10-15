using Pri.Pe1.Timo.Blomme.core.Entities;
using Pri.Pe1.Timo.Blomme.core.Models.RequestModels;
using Pri.Pe1.Timo.Blomme.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Interfaces
{
    public interface IEventService
    {
        IQueryable<Event> GetAll();
        Task<ResultModel<Event>> GetAllAsync();
        Task<ResultModel<Event>> GetByIdAsync(int id);
        Task<BaseResultModel> CreateAsync(EventCreateRequestModel model);
        Task<BaseResultModel> UpdateAsync(EventUpdateRequestModel model);
        Task<BaseResultModel> DeleteAsync(int id);
    }
}
