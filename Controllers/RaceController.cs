using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _raceRepository.GetAll());
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _raceRepository.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            ViewBag.Action = "create";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (ModelState.IsValid)
            {
                race.AppUserId = _httpContextAccessor.HttpContext.User.GetUserId();

                _raceRepository.Add(race);

                return RedirectToAction("Index", "Dashboard");
            }

            return View(race);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Action = "edit";

            return View(await _raceRepository.GetByIdAsync(id.HasValue ? id.Value : 0));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Race race)
        {
            if (ModelState.IsValid)
            {
                _raceRepository.Update(race);

                return RedirectToAction("Index", "Dashboard");
            }

            return View(race);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _raceRepository.GetByIdAsync(id);

            if (clubDetails == null) return View("Error");

            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsync(id);

            if (raceDetails == null)
            {
                return View("Error");
            }

            _raceRepository.Delete(raceDetails);

            return RedirectToAction("Index");
        }
    }
}