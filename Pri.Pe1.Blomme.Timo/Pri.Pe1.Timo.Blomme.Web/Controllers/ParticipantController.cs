using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pri.Pe1.Timo.Blomme.core.Interfaces;
using Pri.Pe1.Timo.Blomme.core.Models.RequestModels;

namespace Pri.Pe1.Timo.Blomme.Web.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;
        private readonly IEventService _eventService;

        public ParticipantController(IParticipantService participantService, IEventService eventService)
        {
            _participantService = participantService;
            _eventService = eventService;
        }

        // GET: Participant
        public async Task<IActionResult> Index()
        {
            var result = await _participantService.GetAllAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to load participants.";
                return View(new List<core.Entities.Participant>());
            }
            return View(result.Items);
        }

        // GET: Participant/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _participantService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Participant not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Items.First());
        }

        // GET: Participant/Create
        public async Task<IActionResult> Create()
        {
            await PopulateEventsViewBag();
            return View();
        }

        // POST: Participant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParticipantCreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateEventsViewBag();
                return View(model);
            }

            var result = await _participantService.CreateAsync(model);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Participant created successfully!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors ?? Enumerable.Empty<string>())
            {
                ModelState.AddModelError("", error);
            }
            await PopulateEventsViewBag();
            return View(model);
        }

        // GET: Participant/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _participantService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Participant not found.";
                return RedirectToAction(nameof(Index));
            }

            var participant = result.Items.First();
            var updateModel = new ParticipantUpdateRequestModel
            {
                Id = participant.Id,
                Name = participant.Name,
                Email = participant.Email,
                EventId = participant.EventId
            };

            await PopulateEventsViewBag();
            return View(updateModel);
        }

        // POST: Participant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ParticipantUpdateRequestModel model)
        {
            if (id != model.Id)
            {
                TempData["Error"] = "Participant ID mismatch.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                await PopulateEventsViewBag();
                return View(model);
            }

            var result = await _participantService.UpdateAsync(model);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Participant updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors ?? Enumerable.Empty<string>())
            {
                ModelState.AddModelError("", error);
            }
            await PopulateEventsViewBag();
            return View(model);
        }

        // GET: Participant/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _participantService.GetByIdAsync(id);
            if (!result.IsSuccess || result.Items == null || !result.Items.Any())
            {
                TempData["Error"] = "Participant not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Items.First());
        }

        // POST: Participant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _participantService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Participant deleted successfully!";
            }
            else
            {
                TempData["Error"] = result.Errors?.FirstOrDefault() ?? "Failed to delete participant.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateEventsViewBag()
        {
            var eventsResult = await _eventService.GetAllAsync();
            if (eventsResult.IsSuccess && eventsResult.Items != null)
            {
                ViewBag.Events = new SelectList(eventsResult.Items, "Id", "Title");
            }
            else
            {
                ViewBag.Events = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
