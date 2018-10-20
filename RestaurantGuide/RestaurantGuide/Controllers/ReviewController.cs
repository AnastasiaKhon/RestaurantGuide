using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantGuide.Models;
using RestaurantGuide.Services;

namespace RestaurantGuide.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public  ActionResult Create(string reviewText, string reviewRating, int placeId)
        {
            try
            {
                var reviewViewModel = new ReviewViewModels()
                {
                    Text = reviewText.Length > 0 ? reviewText : null,
                    Rating = int.Parse(reviewRating),
                    UserId = _userManager.GetUserId(User),
                    PlaceId = placeId
                };
                var reviewModel = _reviewService.AddReview(reviewViewModel);
                return Json(reviewModel);
            }
            catch
            {
                return View();
            }
        }

        //// GET: Review/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Review/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}