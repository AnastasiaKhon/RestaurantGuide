﻿using RestaurantGuide.Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace RestaurantGuide.Models
{
    public class PlaceViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string MainPhotoPath { get; set; }

        public ReviewViewModels Review { get; set; }
        public List<ReviewViewModels> Reviews { get; set; } = new List<ReviewViewModels>();
        public List<PhotoViewModels> Photos { get; set; } = new List<PhotoViewModels>();
    }
}
