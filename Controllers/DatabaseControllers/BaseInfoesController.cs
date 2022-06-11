using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticalTraining.Models.DatabaseMANKA;

namespace PracticalTraining.Controllers.DatabaseControllers
{
    public class BaseInfoesController : Controller
    {
        private readonly MANKAContext _context;

        public BaseInfoesController(MANKAContext context)
        {
            _context = context;
        }

        // GET: BaseInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BaseInfo.ToListAsync());
        }


        // GET: BaseInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseInfo = await _context.BaseInfo.FindAsync(id);
            if (baseInfo == null)
            {
                return NotFound();
            }
            return View(baseInfo);
        }

        // POST: BaseInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BasicText,OwnerPassword,OwnerLogin")] BaseInfo baseInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.BaseInfo.First().BasicText = baseInfo.BasicText;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseInfoExists(baseInfo.OwnerLogin))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(baseInfo);
        }


        private bool BaseInfoExists(string id)
        {
            return _context.BaseInfo.Any(e => e.OwnerLogin == id);
        }
    }
}
