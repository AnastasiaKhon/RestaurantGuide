﻿using RestaurantGuide.Models;
using System;

namespace RestaurantGuide.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsMain { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
