using Microsoft.EntityFrameworkCore;
using RestaurantGuide.Data;
using RestaurantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantGuide.Domain;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace RestaurantGuide.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly FileUploadService _fileUploadService;

        public PlaceService(
            ApplicationContext context, 
            IHostingEnvironment environment, 
            FileUploadService fileUploadService)
        {
            _context = context;
            _environment = environment;
            _fileUploadService = fileUploadService;
        }

        public List<PlaceListItemViewModels> GetPlaces()
        {
            var places = _context.Places
                .Include(p => p.Reviews)
                .Include(p => p.Photos);

            var placeItemList = new List<PlaceListItemViewModels>();

            foreach(var place in places)
            {
                var mainPhoto = place.Photos.FirstOrDefault(p => p.PlaceId == place.Id && p.IsMain);
                var rating = place.Reviews.Count() > 0
                    ? Math.Round(place.Reviews.Sum(r => r.Rating) / (double)place.Reviews.Count(), 1)
                    : 0;

                var item = new PlaceListItemViewModels()
                {
                    Id = place.Id,
                    Title = place.Title,
                    Rating = rating,
                    ReviewCount = place.Reviews.Count(),
                    PhotosCount = place.Photos.Count(),
                    MainPhotoPath = mainPhoto != null ? mainPhoto.FilePath : "images/restaurant-default.png"
                };
                placeItemList.Add(item);
            }

            return placeItemList;
        }

        public PlaceViewModels GetPlace(int id)
        {
            var place = _context.Places
                .Include(p => p.User)
                .Include(p => p.Reviews)
                .Include(p => p.Photos)
                .FirstOrDefault(p => p.Id == id);

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

            var mainPhoto = place.Photos.FirstOrDefault(p => p.PlaceId == place.Id && p.IsMain);

            if(mainPhoto != null)
            {
                placeModel.MainPhotoPath = mainPhoto.FilePath;
            }

            if (place.Photos.Count > 0)
            {
                var photosList = new List<PhotoViewModels>();
                foreach (var photo in place.Photos)
                {
                    var photoItem = new PhotoViewModels()
                    {
                        Id = photo.Id,
                        FileName = photo.FileName,
                        FilePath = photo.FilePath,
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

            if (place.Reviews.Count() > 0)
            {
                var reviewsList = new List<ReviewViewModels>();
                foreach (var review in place.Reviews)
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

                placeModel.Reviews = reviewsList.OrderByDescending(r => r.Date).ToList();
                placeModel.Rating = Math.Round(place.Reviews.Sum(r => r.Rating) / (double)place.Reviews.Count, 1);
            }
            else
            {
                placeModel.Rating = 0;
            }

            return placeModel;
        }

        public int AddPlace(PlaceViewModels placeModel, string userId)
        {
            if (placeModel.MainPhoto == null)
            {
                throw new Exception("Main photo is not set");
            }
            
            var place = new Place()
            {
                Title = placeModel.Title,
                Description = placeModel.Description,
                UserId = userId
            };

            _context.Places.Add(place);
            _context.SaveChanges();
            
            if (placeModel.MainPhoto != null)
            {
                var photo = new Photo()
                {
                    FileName = placeModel.MainPhoto.FileName,
                    FilePath = $"images/{placeModel.MainPhoto.FileName}",
                    UploadDate = DateTime.Now,
                    IsMain = true,
                    UserId = userId,
                    PlaceId = place.Id
                };
                _context.Photos.Add(photo);
                _context.SaveChanges();

                string path = Path.Combine(
                    _environment.WebRootPath,
                    $"images\\");

                _fileUploadService.Upload(path, placeModel.MainPhoto.FileName, placeModel.MainPhoto);
            }

            return place.Id;
        }
    }
}
