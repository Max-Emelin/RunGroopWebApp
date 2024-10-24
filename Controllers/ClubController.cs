using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubRepository clubRepository, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clubRepository.GetAll());
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _clubRepository.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            ViewBag.Action = "create";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (ModelState.IsValid)
            {
                club.AppUserId = _httpContextAccessor.HttpContext.User.GetUserId();

                _clubRepository.Add(club);

                return RedirectToAction("Index", "Dashboard");
            }

            return View(club);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Action = "edit";

            return View(await _clubRepository.GetByIdAsync(id.HasValue ? id.Value : 0));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Club club)
        {
            if (ModelState.IsValid)
            {
                _clubRepository.Update(club);

                return RedirectToAction("Index", "Dashboard");
            }

            return View(club);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);

            if (clubDetails == null) return View("Error");

            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            _clubRepository.Delete(clubDetails);

            return RedirectToAction("Index");
        }
    }
}