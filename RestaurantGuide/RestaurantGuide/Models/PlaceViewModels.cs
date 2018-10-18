using RestaurantGuide.Domain;
using System.Collections.Generic;

namespace RestaurantGuide.Models
{
    public class PlaceViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public List<ReviewViewModels> Reviews { get; set; }
        public List<PhotoViewModels> Photos { get; set; }
    }
}
