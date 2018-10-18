using RestaurantGuide.Domain;
using System.Collections.Generic;

namespace RestaurantGuide.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
