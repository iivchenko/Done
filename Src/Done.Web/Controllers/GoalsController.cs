﻿using System;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Done.Web.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly IRepository<Goal> _goals;
        private readonly UserManager<IdentityUser> _users;

        public GoalsController(UserManager<IdentityUser> users, IRepository<Goal> goals)
        {
            _users = users;
           _goals = goals;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string pattern = "", int page = 1, bool showClosed = false)
        {           
            var goalsCount = 
                _goals
                    .Get(goal => goal.UserId == GetUserId())
                    .Where(x => showClosed ?  true : x.State == State.Open)
                    .Count(x => x.Name.Contains(pattern));

            var totalPages = (int)Math.Ceiling(goalsCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return NotFound($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var goals =
                await _goals
                    // TODO: Think about foreign key and index
                    .Get(x => x.UserId == GetUserId() && x.Name.Contains(pattern))
                    .Where(x => showClosed ?  true : x.State == State.Open)
                    .OrderByDescending(x => x.ModificationDate)
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
        public async Task<ActionResult> New([Bind("Name", "Description")] NewGoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                var g = goal.ToModel();
                g.UserId = GetUserId();

                await _goals.AddAsync(g);

                return RedirectToAction("Index");
            }

            return View(goal);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            var model = await _goals.Get(x => x.UserId == GetUserId() && x.Id == id).SingleAsync();
            return View(model.ToEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id", "Name", "Description", "State")] EditGoalViewModel goal)
        {
            if (ModelState.IsValid)
            {
                var g = _goals.Get(x => x.UserId == GetUserId() && x.Id == goal.Id).Single();

                g.Name = goal.Name;
                g.Description = goal.Description;
                g.ModificationDate = DateTime.UtcNow;
                g.State = goal.State;

                await _goals.UpdateAsync(g);

                return RedirectToAction("Index");
            }

            return View(goal);
        }

        private string GetUserId()
        {
            return _users.GetUserId(HttpContext.User);
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