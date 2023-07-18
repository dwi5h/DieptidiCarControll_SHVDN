using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DieptidiCarControll_SHVDN.Models;
using Newtonsoft.Json;
using GTA.UI;

namespace DieptidiCarControll_SHVDN.Controller
{
    static class StorageController
    {
        public static string DirLocation = Directory.GetCurrentDirectory() + @"\scripts\DieptidiCarControll_SHVDN\Vehicles";

        public static void CreateDirectory()
        {
            if (!Directory.Exists(DirLocation))
            {
                Directory.CreateDirectory(DirLocation);
            }
        }

        public static void SaveVehicle(Kendaraan kendaraan)
        {
            var serializedVehicle = JsonConvert.SerializeObject(kendaraan, Formatting.Indented);

            try
            {
                File.WriteAllText(Path.Combine(DirLocation, GetVehicleFileName(kendaraan)), serializedVehicle, Encoding.UTF8);
                var vehicleFilePath = Path.Combine(DirLocation, GetVehicleFileNameDelete(kendaraan));
                File.Delete(vehicleFilePath);
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't save vehicle file '{GetVehicleFileName(kendaraan)}' to folder '{DirLocation}'. Error message: '{e.Message}'", e);
            }
        }

        public static List<Kendaraan> LoadAllKendaraan()
        {
            string[] fileList = Directory.GetFiles(DirLocation, "*.json");

            List<Kendaraan> garasis = new List<Kendaraan>();

            for (int i = 0; i < fileList.Length; i++)
            {
                try
                {
                    if (!fileList[i].Contains("[isDeleted]"))
                    {
                        garasis.Add(
                            JsonConvert.DeserializeObject<Kendaraan>(
                                File.ReadAllText(fileList[i])
                        ));
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Couldn't load vehicle from file '{fileList[i]}'. Fix or remove this file, and try again. Error message: '{e.Message}'", e);
                }
            };

            return garasis;
        }

        public static void DeleteVehicle(Kendaraan vehicle)
        {
            var vehicleFilePath = Path.Combine(DirLocation, GetVehicleFileName(vehicle));

            if (!File.Exists(vehicleFilePath))
                return;

            try
            {
                File.Delete(vehicleFilePath);
                var serializedVehicle = JsonConvert.SerializeObject(vehicle, Formatting.Indented);
                File.WriteAllText(Path.Combine(DirLocation, GetVehicleFileNameDelete(vehicle)), serializedVehicle, Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't delete vehicle file '{GetVehicleFileName(vehicle)}' from folder '{DirLocation}. Error message: '{e.Message}'", e);
            }
        }

        private static string GetVehicleFileName(Kendaraan vehicle)
        {
            return $"{vehicle.ModelName}-{vehicle.LicensePlate}.json";
        }

        private static string GetVehicleFileNameDelete(Kendaraan vehicle)
        {
            return $"{vehicle.ModelName}-{vehicle.LicensePlate}-[isDeleted].json";
        }
    }
}
