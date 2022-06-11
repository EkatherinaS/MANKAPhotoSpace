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
    public class PlaceInfoesController : Controller
    {
        private readonly MANKAContext _context;

        public PlaceInfoesController(MANKAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            MANKAContext dbConnection = new MANKAContext();

            List<PlaceInfoModel> placeInfo = dbConnection.PlaceInfo.Where(p=>p.PlaceCloseDate==null).Select(p=> new PlaceInfoModel(p)).ToList();
            return View(placeInfo);
        }

        public IActionResult Create()
        {
            PlaceInfoModel pm = new PlaceInfoModel();
            pm.CreateBase();
            return View(pm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaceName, MaxPeopleNumber, City, Street, Building")] PlaceInfoModel placeInfoModel)
        {
            short addressCode = placeInfoModel.UpdateAddress();
            PlaceInfo placeInfo = new PlaceInfo();
            placeInfo.AddressCode = addressCode;
            placeInfo.MaxPeopleNumber = placeInfoModel.MaxPeopleNumber;
            placeInfo.PlaceName = placeInfoModel.PlaceName;

            placeInfoModel.CreateBase();

            _context.Add(placeInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeInfo = await _context.PlaceInfo.FindAsync(id);
            if (placeInfo == null)
            {
                return NotFound();
            }

            PlaceInfoModel model = new PlaceInfoModel(placeInfo);
            model.CreateBase();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("PlaceCode, PlaceName, MaxPeopleNumber, City, Street, Building")] PlaceInfoModel placeInfoModel)
        {
            PlaceInfo placeInfo = new PlaceInfo();
            short addressCode = placeInfoModel.UpdateAddress();
            placeInfo.AddressCode = addressCode;
            placeInfo.MaxPeopleNumber = placeInfoModel.MaxPeopleNumber;
            placeInfo.PlaceName = placeInfoModel.PlaceName;
            placeInfo.PlaceCode = placeInfoModel.PlaceCode;
            
            placeInfoModel.CreateBase();
            try
            {
                _context.Update(placeInfo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceInfoExists(placeInfo.PlaceCode))
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


        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeInfo = await _context.PlaceInfo
                .Include(p => p.AddressCodeNavigation)
                .FirstOrDefaultAsync(m => m.PlaceCode == id);
            if (placeInfo == null)
            {
                return NotFound();
            }

            return View(new PlaceInfoModel(placeInfo));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            _context.PlaceInfo.FirstOrDefault(s => s.PlaceCode == id).PlaceCloseDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PlaceInfoExists(short id)
        {
            return _context.PlaceInfo.Any(e => e.PlaceCode == id);
        }
    }
}
