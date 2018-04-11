using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyTaskList.Models;
using MyTaskList.Models.Tasks;

namespace MyTaskList.Controllers
{
    public class TasksController : Controller
    {
        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly TasksContext _tasksContext = new TasksContext();

        [HttpGet]
        public async Task<ActionResult> Index(string pattern = "", int page = 1)
        {
            var tasksCount = _tasksContext.Tasks.Count(x => x.Name.Contains(pattern));
            var totalPages = (int)Math.Ceiling(tasksCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return new HttpNotFoundResult($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var tasks =
                await _tasksContext
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
        public async Task<ActionResult> New(TaskItem task)
        {
            _tasksContext.Tasks.Add(task);
            await _tasksContext.SaveChangesAsync();

            return RedirectToAction("Index", await _tasksContext.Tasks.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            return View(await _tasksContext.Tasks.SingleAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TaskItem task)
        {
            _tasksContext.Entry(task).State = EntityState.Modified;

            await _tasksContext.SaveChangesAsync();

            return RedirectToAction("Index", await _tasksContext.Tasks.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tasksContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}