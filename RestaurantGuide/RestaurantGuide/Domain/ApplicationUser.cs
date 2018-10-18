using Microsoft.AspNetCore.Identity;
using RestaurantGuide.Domain;
using System.Collections.Generic;

namespace RestaurantGuide.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Place> Places { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
