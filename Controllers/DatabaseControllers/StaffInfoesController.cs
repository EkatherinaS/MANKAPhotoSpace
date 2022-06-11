using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticalTraining.Models;
using PracticalTraining.Models.DatabaseMANKA;

namespace PracticalTraining.Controllers.DatabaseControllers
{
    public class StaffInfoesController : Controller
    {
        private readonly MANKAContext _context;

        public StaffInfoesController(MANKAContext context)
        {
            _context = context;
        }

        // GET: StaffInfoes
        public async Task<IActionResult> Index()
        {
            List<StaffInfo> list = await _context.StaffInfo.ToListAsync();
            list.ForEach(s => s.StaffPhoneView = PhoneNumber.PhoneNumberNormalView(s.StaffPhone));
            return View(list);
        }

        // GET: StaffInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffPhone,PassportSeries,PassportNumber,Snils,PaymentPerHour,StaffFamilyName,StaffName,StaffSurname,ResignationDate,StaffPassword")] StaffInfo staffInfo)
        {
            if (ModelState.IsValid)
            {
                staffInfo.StaffPhone = PhoneNumber.PhoneNumberDatabaseView(staffInfo.StaffPhone);
                _context.Add(staffInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffInfo);
        }

        // GET: StaffInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            id = PhoneNumber.PhoneNumberDatabaseView(id);

            var staffInfo = await _context.StaffInfo.FindAsync(id);
            if (staffInfo == null)
            {
                return NotFound();
            }
            staffInfo.StaffPhone = PhoneNumber.PhoneNumberNormalView(staffInfo.StaffPhone);
            return View(staffInfo);
        }

        // POST: StaffInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StaffPhone,StaffPassword,PassportSeries,PassportNumber,Snils,PaymentPerHour,StaffFamilyName,StaffName,StaffSurname,ResignationDate")] StaffInfo staffInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    staffInfo.StaffPhone = PhoneNumber.PhoneNumberDatabaseView(staffInfo.StaffPhone);
                    _context.Update(staffInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffInfoExists(staffInfo.StaffPhone))
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
            return View(staffInfo);
        }

        // GET: StaffInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffInfo = await _context.StaffInfo
                .FirstOrDefaultAsync(m => m.StaffPhone == id);
            if (staffInfo == null)
            {
                return NotFound();
            }
            staffInfo.StaffPhone = PhoneNumber.PhoneNumberNormalView(staffInfo.StaffPhone);

            return View(staffInfo);
        }

        // POST: StaffInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _context.StaffInfo.FirstOrDefault(s => s.StaffPhone == id).ResignationDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: StaffInfoes/Delete/5
        public async Task<IActionResult> TakeBack(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffInfo = await _context.StaffInfo
                .FirstOrDefaultAsync(m => m.StaffPhone == id);
            if (staffInfo == null)
            {
                return NotFound();
            }
            staffInfo.StaffPhone = PhoneNumber.PhoneNumberNormalView(staffInfo.StaffPhone);

            return View(staffInfo);
        }

        // POST: StaffInfoes/Delete/5
        [HttpPost, ActionName("TakeBack")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeBackConfirmed(string id)
        {
            _context.StaffInfo.FirstOrDefault(s => s.StaffPhone == id).ResignationDate = null;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public string GetId(string phone)
        {
            return PhoneNumber.PhoneNumberDatabaseView(phone);
        }

        private bool StaffInfoExists(string id)
        {
            return _context.StaffInfo.Any(e => e.StaffPhone == id);
        }
    }
}
