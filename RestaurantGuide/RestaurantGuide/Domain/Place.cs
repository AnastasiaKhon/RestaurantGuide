using RestaurantGuide.Domain;
using System.Collections.Generic;

namespace RestaurantGuide.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
