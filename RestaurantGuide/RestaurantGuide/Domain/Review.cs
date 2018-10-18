using RestaurantGuide.Models;
using System;

namespace RestaurantGuide.Domain
{
    public class Review
    {
        public int Id { get; set; }

        public int Text { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }

        public string UserId { get; set; }
        public virtual  ApplicationUser User { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
