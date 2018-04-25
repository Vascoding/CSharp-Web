using CameraStore.Data;
using CameraStore.Data.Models.CameraViewModels;
using CameraStore.Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using CameraStore.Data.Enumerations;
using CameraStore.Data.Models;
using System;

namespace CameraStore.Service.Implementations
{
    public class CameraService : ICameraService
    {
        private readonly CameraDbContext db;

        public CameraService(CameraDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraModel> All()
        {
            using (this.db)
            {
                return this.db.Cameras
                    .Select(c => new CameraModel
                    {
                        Id = c.Id,
                        Make = c.Make.ToString(),
                        ImageUrl = c.ImageUrl,
                        Model = c.Model,
                        Price = c.Price,
                        Quantity = c.Quantity
                    }).ToList();
            }
        }
        
        public void Create(CameraMake make,
            string model, 
            double price, 
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed, 
            int minIso,
            int maxIso, 
            bool isFullFrame, 
            string videoResolution,
            IEnumerable<LightMatering> lightMatering,
            string description,
            string imageUrl,
            string userId)
        {
            using (this.db)
            {
                var camera = new Camera
                {
                    Make = make,
                    Model = model,
                    Price = price,
                    Quantity = quantity,
                    MinShutterSpeed = minShutterSpeed,
                    MaxShutterSpeed = maxShutterSpeed,
                    MinIso = minIso,
                    MaxIso = maxIso,
                    IsFullFrame = isFullFrame,
                    VideoResolution = videoResolution, 
                    LightMatering = (LightMatering)lightMatering.Cast<int>().Sum(),
                    Description = description,
                    ImageUrl = imageUrl,
                    UserId = userId
                };

                this.db.Cameras.Add(camera);
                this.db.SaveChanges();
            }
        }

        public CameraDetailsModel Details(int id)
        {
            using (this.db)
            {
                return this.db.Cameras
                    .Where(c => c.Id == id)
                    .Select(c => new CameraDetailsModel
                    {
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        IsFullFrame = c.IsFullFrame,
                        LightMatering = SetLightMetering(c.LightMatering),
                        Make = c.Make,
                        MaxIso = c.MaxIso,
                        MaxShutterSpeed = c.MaxShutterSpeed,
                        MinIso = c.MinIso,
                        MinShutterSpeed = c.MinShutterSpeed,
                        Model = c.Model,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        SellerUsername = c.User.UserName,
                        SellerId = c.UserId,
                        VideoResolution = c.VideoResolution
                    }).FirstOrDefault();
            }
        }

        public CameraDetailsModel Find(int id)
        {
            using (this.db)
            {
                return db.Cameras
                    .Where(c => c.Id == id)
                    .Select(c => new CameraDetailsModel
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        MinShutterSpeed = c.MinShutterSpeed,
                        MaxShutterSpeed = c.MaxShutterSpeed,
                        MinIso = c.MinIso,
                        MaxIso = c.MaxIso,
                        IsFullFrame = c.IsFullFrame,
                        VideoResolution = c.VideoResolution,
                        LightMatering = SetLightMetering(c.LightMatering),
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                    }).FirstOrDefault();
            }
        }

        public bool Edit(int id, AddCameraModel model)
        {
            using (this.db)
            {
                var camera = this.db.Cameras.FirstOrDefault(c => c.Id == id);

                if (camera != null)
                {
                    camera.Description = model.Description;
                    camera.ImageUrl = model.ImageUrl;
                    camera.IsFullFrame = model.IsFullFrame;
                    camera.LightMatering = (LightMatering)model.LightMatering.Cast<int>().Sum();
                    camera.Make = model.Make;
                    camera.MaxIso = model.MaxIso;
                    camera.MaxShutterSpeed = model.MaxShutterSpeed;
                    camera.MinIso = model.MinIso;
                    camera.MinShutterSpeed = model.MinShutterSpeed;
                    camera.Model = model.Model;
                    camera.Price = model.Price;
                    camera.Quantity = model.Quantity;
                    camera.VideoResolution = model.VideoResolution;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void Delete(int id)
        {
            using (this.db)
            {
                var camera = this.db.Cameras.FirstOrDefault(c => c.Id == id);

                if (camera != null)
                {
                    this.db.Cameras.Remove(camera);
                    this.db.SaveChanges();
                }
            }
        }

        private IEnumerable<LightMatering> SetLightMetering(LightMatering lightMatering)
        {
            var sum = (int)lightMatering;
            List<LightMatering> lightMaterings = new List<LightMatering>();
            if (sum == 3)
            {
                lightMaterings.Add(LightMatering.Spot);
                lightMaterings.Add(LightMatering.CenterWeighted);
            }
            else if (sum == 5)
            {
                lightMaterings.Add(LightMatering.Spot);
                lightMaterings.Add(LightMatering.Evaluative);
            }

            else if (sum == 6)
            {
                lightMaterings.Add(LightMatering.CenterWeighted);
                lightMaterings.Add(LightMatering.Evaluative);
            }
            else if (sum == 7)
            {
                lightMaterings.Add(LightMatering.Spot);
                lightMaterings.Add(LightMatering.CenterWeighted);
                lightMaterings.Add(LightMatering.Evaluative);
            }
            else
            {
                lightMaterings.Add((LightMatering)sum);
            }
            return lightMaterings;
        }
    }
}
