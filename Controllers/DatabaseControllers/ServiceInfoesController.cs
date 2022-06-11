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
    public class ServiceInfoesController : Controller
    {
        private readonly MANKAContext _context;

        public ServiceInfoesController(MANKAContext context)
        {
            _context = context;
        }

        // GET: ServiceInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceInfo.Where(S => S.DateExploration == null).ToListAsync());
        }

        // GET: ServiceInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceCode,ServiceName,ServicePrice,DateExploration")] ServiceInfo serviceInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceInfo);
        }

        // GET: ServiceInfoes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceInfo = await _context.ServiceInfo.FindAsync(id);
            if (serviceInfo == null)
            {
                return NotFound();
            }
            return View(serviceInfo);
        }

        // POST: ServiceInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ServiceCode,ServiceName,ServicePrice,DateExploration")] ServiceInfo serviceInfo)
        {
            if (id != serviceInfo.ServiceCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceInfoExists(serviceInfo.ServiceCode))
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
            return View(serviceInfo);
        }

        // GET: ServiceInfoes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceInfo = await _context.ServiceInfo
                .FirstOrDefaultAsync(m => m.ServiceCode == id);
            if (serviceInfo == null)
            {
                return NotFound();
            }

            return View(serviceInfo);
        }

        // POST: ServiceInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            _context.ServiceInfo.FirstOrDefault(s => s.ServiceCode == id).DateExploration = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceInfoExists(short id)
        {
            return _context.ServiceInfo.Any(e => e.ServiceCode == id);
        }
    }
}
