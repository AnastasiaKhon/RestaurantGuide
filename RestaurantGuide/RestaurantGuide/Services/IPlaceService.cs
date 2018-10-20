using Microsoft.AspNetCore.Http;
using RestaurantGuide.Models;
using System.Collections.Generic;

namespace RestaurantGuide.Services
{
    public interface IPlaceService
    {
        List<PlaceListItemViewModels> GetPlaces();
        PlaceViewModels GetPlace(int id);
        int AddPlace(PlaceViewModels placeModel, string userId);
        void UploadPhotosToPlace(List<IFormFile> files, int placeId, string userId);
    }
}
