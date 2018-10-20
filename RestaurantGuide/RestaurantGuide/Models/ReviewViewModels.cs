using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantGuide.Models
{
    public class ReviewViewModels
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public int PlaceId { get; set; }
    }
}
