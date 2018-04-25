using CameraStore.Data.Enumerations;
using CameraStore.Data.Models.CameraViewModels;
using System.Collections.Generic;

namespace CameraStore.Service.Contracts
{
    public interface ICameraService
    {
        IEnumerable<CameraModel> All();

        void Create(CameraMake make, 
            string model, 
            double price, 
            int quantity, 
            int minShutterSpeel, 
            int maxShutterSpeed, 
            int minIso, int maxIso, 
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMatering> lightMatering,
            string description,
            string imageUrl,
            string userId);

        CameraDetailsModel Details(int id);

        CameraDetailsModel Find(int id);

        bool Edit(int id, AddCameraModel model);

        void Delete(int id);
    }
}
