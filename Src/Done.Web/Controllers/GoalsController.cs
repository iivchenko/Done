using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Done.Web.Models;
using Done.Web.Models.Tasks;

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
            var tasksCount = _goalsContext.Tasks.Count(x => x.Name.Contains(pattern));
            var totalPages = (int)Math.Ceiling(tasksCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return new HttpNotFoundResult($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var tasks =
                await _goalsContext
                    .Tasks
                    .Where(x => x.Name.Contains(pattern))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).ToListAsync();

            var indexVm = new IndexViewModel
            {
                Total = tasksCount,
                Pagination = new Models.Pagination.PageViewModel(totalPages, PagesCount, page),
                Tasks = tasks,
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
        public async Task<ActionResult> New(Goal task)
        {
            _goalsContext.Tasks.Add(task);
            await _goalsContext.SaveChangesAsync();

            return RedirectToAction("Index", await _goalsContext.Tasks.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            return View(await _goalsContext.Tasks.SingleAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Goal task)
        {
            _goalsContext.Entry(task).State = EntityState.Modified;

            await _goalsContext.SaveChangesAsync();

            return RedirectToAction("Index", await _goalsContext.Tasks.ToListAsync());
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