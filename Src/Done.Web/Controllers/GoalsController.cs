using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Done.Web.Models;
using Done.Web.Models.Data;
using Done.Web.Models.ViewModels.Goals;
using Done.Web.Models.ViewModels.Pagination;

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
                Pagination = new PageViewModel(totalPages, PagesCount, page),
                Goals = goals.Select(x => x.ToViewModel()),
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New([Bind(Include = "Id,Name,Description,State")] GoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                _goalsContext.Goals.Add(goal.ToModel());
                await _goalsContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(goal);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            var model = await _goalsContext.Goals.SingleAsync(x => x.Id == id);
            return View(model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,State")] GoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                _goalsContext.Entry(goal.ToModel()).State = EntityState.Modified;

                await _goalsContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(goal);
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