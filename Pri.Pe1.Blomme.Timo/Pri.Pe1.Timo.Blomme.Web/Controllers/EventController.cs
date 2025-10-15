using Microsoft.AspNetCore.Mvc;
using Pri.Pe1.Timo.Blomme.core.Interfaces;
using Pri.Pe1.Timo.Blomme.core.Models.RequestModels;

namespace Pri.Pe1.Timo.Blomme.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var result = await _eventService.GetAllAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to load events.";
                return View(new List<core.Entities.Event>());
            }
            return View(result.Items);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Event not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Items.First());
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _eventService.CreateAsync(model);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Event created successfully!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors ?? Enumerable.Empty<string>())
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Event not found.";
                return RedirectToAction(nameof(Index));
            }

            var eventEntity = result.Items.First();
            var updateModel = new EventUpdateRequestModel
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                Location = eventEntity.Location,
                MaxParticipants = eventEntity.MaxParticipants
            };

            return View(updateModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventUpdateRequestModel model)
        {
            if (id != model.Id)
            {
                TempData["Error"] = "Event ID mismatch.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _eventService.UpdateAsync(model);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Event updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors ?? Enumerable.Empty<string>())
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Event not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Items.First());
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _eventService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Event deleted successfully!";
            }
            else
            {
                TempData["Error"] = result.Errors?.FirstOrDefault() ?? "Failed to delete event.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
