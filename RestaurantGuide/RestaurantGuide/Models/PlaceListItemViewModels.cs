using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantGuide.Models
{
    public class PlaceListItemViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Rating { get; set; }
        public int PhotosCount { get; set; }
        public int ReviewCount { get; set; }

    }
}
