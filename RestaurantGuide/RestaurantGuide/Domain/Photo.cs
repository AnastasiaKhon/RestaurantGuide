using RestaurantGuide.Models;

namespace RestaurantGuide.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
