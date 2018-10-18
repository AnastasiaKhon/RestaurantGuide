using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantGuide.Models
{
    public class ReviewViewModels
    {
        public int Id { get; set; }

        public int Text { get; set; }
        public DateTime Date { get; set; }
        public double Rating { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public int PlaceId { get; set; }
    }
}
