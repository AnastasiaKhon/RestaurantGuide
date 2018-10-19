using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantGuide.Models
{
    public class PlaceListViewModel
    {
        public List<PlaceListItemViewModels> Places { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
