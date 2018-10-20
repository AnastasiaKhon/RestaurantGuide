using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPlaceService _placeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlaceController(IPlaceService placeService, UserManager<ApplicationUser> userManager)
        {
            _placeService = placeService;
            _userManager = userManager;
        }

        // GET: Place
        public IActionResult Index(int page=1)
        {
            int pageSize = 3;
            var source = _placeService.GetPlaces();
            var count = source.Count;
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PlaceListViewModel viewModel = new PlaceListViewModel
            {
                PageViewModel = pageViewModel,
                Places = items
            };
            return View(viewModel);
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
        public IActionResult Create(PlaceViewModels placeModel)
        {
            if (ModelState.IsValid)
            {
                // var currentUser = await _userManager.(HttpContext.User);
                var placeId = _placeService.AddPlace(placeModel, _userManager.GetUserId(User));
                return RedirectToAction(nameof(Index));
            }
            return View(placeModel);
        }
        
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
