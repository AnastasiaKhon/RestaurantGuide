﻿using System;

namespace RestaurantGuide.Models
{
    public class PhotoViewModels
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsMain { get; set; }
        public DateTime UploadDate { get; set; } 

        public int PlaceId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
