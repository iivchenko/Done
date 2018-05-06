using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Done.Core.Data;
using Done.Data;
using Done.Domain;
using Done.Web.Models;
using Done.Web.Models.Goals;
using Done.Core.Web.Pagination;
using Microsoft.Extensions.Configuration;

namespace Done.Web.Controllers
{
    public class GoalsController : Controller
    {
        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly IRepository<Goal> _goals;

        public GoalsController(IRepository<Goal> goals)
        {
           _goals = goals;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string pattern = "", int page = 1)
        {
            var goalsCount = _goals.Get(goal => true).Count(x => x.Name.Contains(pattern));
            var totalPages = (int)Math.Ceiling(goalsCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return NotFound($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var goals =
                await _goals
                    .Get(x => x.Name.Contains(pattern))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

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
        public async Task<ActionResult> New([Bind("Name", "Description", "State")] GoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                await _goals.AddAsync(goal.ToModel());

                return RedirectToAction("Index");
            }

            return View(goal);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            var model = await _goals.Get(x => x.Id == id).SingleAsync();
            return View(model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id", "Name", "Description", "State")] GoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                await _goals.UpdateAsync(goal.ToModel());

                return RedirectToAction("Index");
            }

            return View(goal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _goals.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}