using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantGuide.Data;
using RestaurantGuide.Domain;
using RestaurantGuide.Models;
using RestaurantGuide.Services;

namespace RestaurantGuide.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly PlaceService _placeService;

        public PlaceController()
        {
            _placeService = new PlaceService(_context);
        }

        // GET: Place
        public IActionResult Index()
        {
            return View(_placeService.GetPlaces());
        }

        // GET: Place/Details/5
        public IActionResult Details(int id)
        {
            var placeListItemViewModels = _placeService.GetPlace(id);
            if (placeListItemViewModels == null)
            {
                return NotFound();
            }

            return View(placeListItemViewModels);
        }

        // GET: Place/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,MainPhoto")] PlaceViewModels placeModel)
        {
            if (ModelState.IsValid)
            {
                var placeId = _placeService.AddPlace(placeModel);
                if (placeModel.MainPhoto == null)
                {
                    throw new Exception("Main photo is not set");
                }

                //var place = new Place()
                //{
                //    Title = placeModel.Title,
                //    Description = placeModel.Description,
                //    UserId = placeModel.UserId
                //};

                //_context.Add(place);
                //_context.SaveChanges();

                //if (placeModel.MainPhoto != null)
                //{
                //    var photo = new Photo()
                //    {
                //        Name = placeModel.MainPhoto.Name,
                //        UploadDate = DateTime.Now,
                //        IsMain = placeModel.MainPhoto.IsMain,
                //        UserId = placeModel.UserId,
                //        PlaceId = place.Id
                //    };
                //    _context.Add(photo);
                //    _context.SaveChanges();
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(placeModel);
        }

        // GET: Place/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var placeListItemViewModels = await _context.PlaceListItemViewModels.SingleOrDefaultAsync(m => m.Id == id);
        //    if (placeListItemViewModels == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(placeListItemViewModels);
        //}

        // POST: Place/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Rating,PhotosCount,ReviewCount")] PlaceListItemViewModels placeListItemViewModels)
        //{
        //    if (id != placeListItemViewModels.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(placeListItemViewModels);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PlaceListItemViewModelsExists(placeListItemViewModels.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(placeListItemViewModels);
        //}

        // GET: Place/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var placeListItemViewModels = await _context.PlaceListItemViewModels
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (placeListItemViewModels == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(placeListItemViewModels);
        //}

        // POST: Place/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var placeListItemViewModels = await _context.PlaceListItemViewModels.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.PlaceListItemViewModels.Remove(placeListItemViewModels);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PlaceListItemViewModelsExists(int id)
        //{
        //    return _context.PlaceListItemViewModels.Any(e => e.Id == id);
        //}
    }
}
