using Microsoft.EntityFrameworkCore;
using Pri.Pe1.Timo.Blomme.core.Entities;
using Pri.Pe1.Timo.Blomme.core.Interfaces;
using Pri.Pe1.Timo.Blomme.core.Models.RequestModels;
using Pri.Pe1.Timo.Blomme.core.Models;
using Pri.Pe1.Timo.Blomme.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly AppDbContext _context;

        public ParticipantService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Participant> GetAll() => _context.Participants.AsQueryable();

        public async Task<ResultModel<Participant>> GetAllAsync() =>
            new ResultModel<Participant>
            {
                IsSuccess = true,
                Items = await _context.Participants.Include(p => p.Event).ToListAsync()
            };

        public async Task<ResultModel<Participant>> GetByIdAsync(int id)
        {
            Participant? participant = await _context.Participants.Include(p => p.Event).FirstOrDefaultAsync(p => p.Id == id);
            if (participant == null)
                return new ResultModel<Participant> { IsSuccess = false, Errors = new[] { "Participant not found" } };

            return new ResultModel<Participant> { IsSuccess = true, Items = new[] { participant } };
        }

        public async Task<BaseResultModel> CreateAsync(ParticipantCreateRequestModel model)
        {
            if (!await _context.Events.AnyAsync(e => e.Id == model.EventId))
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Event not found" } };

            Participant? participant = new Participant
            {
                Name = model.Name,
                Email = model.Email,
                EventId = model.EventId
            };

            _context.Participants.Add(participant);
            return await SaveChangesAsync();
        }

        public async Task<BaseResultModel> UpdateAsync(ParticipantUpdateRequestModel model)
        {
            Participant? participant = await _context.Participants.FindAsync(model.Id);
            if (participant == null)
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Participant not found" } };

            participant.Name = model.Name;
            participant.Email = model.Email;
            participant.EventId = model.EventId;

            return await SaveChangesAsync();
        }

        public async Task<BaseResultModel> DeleteAsync(int id)
        {
            Participant? participant = await _context.Participants.FindAsync(id);
            if (participant == null)
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Participant not found" } };

            _context.Participants.Remove(participant);
            return await SaveChangesAsync();
        }

        private async Task<BaseResultModel> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return new BaseResultModel { IsSuccess = true };
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Database error" } };
            }
        }
    }
}
