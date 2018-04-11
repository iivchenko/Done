using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Done.Web.Models;
using Done.Web.Models.Goals;

namespace Done.Web.Controllers
{
    public class GoalsController : Controller
    {
        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly GoalsContext _goalsContext = new GoalsContext();

        [HttpGet]
        public async Task<ActionResult> Index(string pattern = "", int page = 1)
        {
            var goalsCount = _goalsContext.Goals.Count(x => x.Name.Contains(pattern));
            var totalPages = (int)Math.Ceiling(goalsCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return new HttpNotFoundResult($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var goals =
                await _goalsContext
                    .Goals
                    .Where(x => x.Name.Contains(pattern))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).ToListAsync();

            var indexVm = new IndexViewModel
            {
                Total = goalsCount,
                Pagination = new Models.Pagination.PageViewModel(totalPages, PagesCount, page),
                Goals = goals,
                Pattern = pattern
            };

            return View("Index", indexVm);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(Goal goal)
        {
            _goalsContext.Goals.Add(goal);
            await _goalsContext.SaveChangesAsync();

            return RedirectToAction("Index", await _goalsContext.Goals.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            return View(await _goalsContext.Goals.SingleAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Goal goal)
        {
            _goalsContext.Entry(goal).State = EntityState.Modified;

            await _goalsContext.SaveChangesAsync();

            return RedirectToAction("Index", await _goalsContext.Goals.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _goalsContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}