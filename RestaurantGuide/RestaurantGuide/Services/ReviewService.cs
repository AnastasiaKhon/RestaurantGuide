using Microsoft.AspNetCore.Hosting;
using RestaurantGuide.Data;
using RestaurantGuide.Domain;
using RestaurantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantGuide.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly FileUploadService _fileUploadService;

        public ReviewService(
            ApplicationContext context,
            IHostingEnvironment environment,
            FileUploadService fileUploadService)
        {
            _context = context;
            _environment = environment;
            _fileUploadService = fileUploadService;
        }

        public List<ReviewViewModels> GetReviews(int placeId)
        {
            var reviews = _context.Reviews.Where(r => r.PlaceId == placeId);

            var reviewList = new List<ReviewViewModels>();

            foreach (var review in reviews)
            {
                var item = new ReviewViewModels()
                {
                    Id = review.Id,
                    Text = review.Text,
                    Date = review.Date,
                    UserName = review.User.UserName
                };
                reviewList.Add(item);
            }

            return reviewList;
        }

        public ReviewViewModels AddReview(ReviewViewModels reviewModel)
        { 
            var review = new Review()
            {
                Text = reviewModel.Text,
                Rating = Convert.ToInt32(reviewModel.Rating),
                Date = DateTime.Now,
                UserId = reviewModel.UserId,
                PlaceId = reviewModel.PlaceId
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            var user = _context.Users.FirstOrDefault(u => u.Id == review.UserId);

            reviewModel.Id = review.Id;
            reviewModel.Date = review.Date;
            reviewModel.UserName = user != null ? user.UserName : "";

            return reviewModel;
        }
    }
}
