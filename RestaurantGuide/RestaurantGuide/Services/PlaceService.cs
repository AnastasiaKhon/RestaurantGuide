using Microsoft.EntityFrameworkCore;
using RestaurantGuide.Data;
using RestaurantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantGuide.Services
{
    public class PlaceService : IPlaceService
    {
        protected readonly ApplicationContext _context;

        public PlaceService(ApplicationContext context)
        {
            _context = context;
        }

        public List<PlaceListItemViewModels> GetPlaces()
        {
            var places = _context.Places
                .Include(p => p.Reviews)
                .Include(p => p.Photos);

            var placeItemList = new List<PlaceListItemViewModels>();

            foreach(var place in places)
            {
                var item = new PlaceListItemViewModels()
                {
                    Id = place.Id,
                    Title = place.Title,
                    Rating = Math.Round(place.Reviews.Sum(r => r.Rating) / (double)place.Reviews.Count(), 1),
                    ReviewCount = place.Reviews.Count(),
                    PhotosCount = place.Photos.Count()
                };
                placeItemList.Add(item);
            }

            return placeItemList;
        }

        public PlaceViewModels GetPlace(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.Id == id);

            if (place == null)
            {
                throw new Exception("Place does not found");
            }

            var placeModel = new PlaceViewModels()
            {
                Id = place.Id,
                Title = place.Title, 
                Description = place.Description, 
                UserId = place.UserId,
                UserName = place.User.UserName
            };

            var reviews = _context.Reviews.Where(r => r.PlaceId == place.Id).ToList();

            if (reviews.Count() > 0)
            {
                var reviewsList = new List<ReviewViewModels>();
                foreach (var review in reviews)
                {
                    var reviewItem = new ReviewViewModels()
                    {
                        Id = review.Id,
                        Text = review.Text,
                        Date = review.Date,
                        Rating = review.Rating,
                        UserId = review.UserId,
                        UserName = review.User.UserName,
                        PlaceId = review.PlaceId
                    };
                    reviewsList.Add(reviewItem);
                }

                placeModel.Reviews = reviewsList;
            }

            var photos = _context.Photos.Where(p => p.PlaceId == place.Id).ToList();

            if(photos.Count() > 0)
            {
                var photosList = new List<PhotoViewModels>();
                foreach(var photo in photos)
                {
                    var photoItem = new PhotoViewModels()
                    {
                        Id = photo.Id,
                        Name = photo.Name,
                        IsMain = photo.IsMain,
                        UploadDate = photo.UploadDate,
                        PlaceId = photo.PlaceId,
                        UserId = photo.UserId,
                        UserName = photo.User.UserName
                    };
                    photosList.Add(photoItem);
                }

                placeModel.Photos = photosList;
            }

            return placeModel;
        }

        //public int AddPlace(PlaceViewModels placeModel)
        //{

        //}
    }
}
