using RestaurantGuide.Models;

namespace RestaurantGuide.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
