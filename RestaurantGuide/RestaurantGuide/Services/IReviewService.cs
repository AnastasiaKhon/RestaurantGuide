using RestaurantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantGuide.Services
{
    public interface IReviewService
    {
        List<ReviewViewModels> GetReviews(int placeId);
        ReviewViewModels AddReview(ReviewViewModels reviewModel);
    }
}
