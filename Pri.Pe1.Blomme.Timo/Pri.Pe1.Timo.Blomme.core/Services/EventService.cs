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
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetAll() => _context.Events.AsQueryable();

        public async Task<ResultModel<Event>> GetAllAsync() =>
            new ResultModel<Event>
            {
                IsSuccess = true,
                Items = await _context.Events.Include(e => e.Participants).ToListAsync()
            };

        public async Task<ResultModel<Event>> GetByIdAsync(int id)
        {
            var ev = await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null)
                return new ResultModel<Event> { IsSuccess = false, Errors = new[] { "Event not found" } };

            return new ResultModel<Event> { IsSuccess = true, Items = new List<Event> { ev } };
        }

        public async Task<BaseResultModel> CreateAsync(EventCreateRequestModel model)
        {
            if (await _context.Events.AnyAsync(e => e.Title.ToUpper() == model.Title.ToUpper()))
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Event title already exists" } };

            var ev = new Event
            {
                Title = model.Title,
                Date = model.Date,
                Location = model.Location,
                MaxParticipants = model.MaxParticipants
            };

            _context.Events.Add(ev);
            return await SaveChangesAsync();
        }

        public async Task<BaseResultModel> UpdateAsync(EventUpdateRequestModel model)
        {
            var ev = await _context.Events.FindAsync(model.Id);
            if (ev == null)
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Event not found" } };

            ev.Title = model.Title;
            ev.Date = model.Date;
            ev.Location = model.Location;
            ev.MaxParticipants = model.MaxParticipants;

            return await SaveChangesAsync();
        }

        public async Task<BaseResultModel> DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return new BaseResultModel { IsSuccess = false, Errors = new[] { "Event not found" } };

            _context.Events.Remove(ev);
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
