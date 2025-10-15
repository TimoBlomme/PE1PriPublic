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
    public interface IParticipantService
    {
        IQueryable<Participant> GetAll();
        Task<ResultModel<Participant>> GetAllAsync();
        Task<ResultModel<Participant>> GetByIdAsync(int id);
        Task<BaseResultModel> CreateAsync(ParticipantCreateRequestModel model);
        Task<BaseResultModel> UpdateAsync(ParticipantUpdateRequestModel model);
        Task<BaseResultModel> DeleteAsync(int id);
    }
}
