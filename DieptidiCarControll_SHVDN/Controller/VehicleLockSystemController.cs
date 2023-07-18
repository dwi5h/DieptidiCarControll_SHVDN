using System;
using System.Collections.Generic;
using System.Linq;
using DieptidiUtility_SHVDN;
using DieptidiCarControll_SHVDN.Models;
using GTA;
using GTA.Math;
using GTA.UI;

namespace DieptidiCarControll_SHVDN.Controller
{
    public static class VehicleLockSystemController
    {
        public static void Lock(List<Vehicle> vehicles, List<Blip> blips)
        {
            var veh = Helper.GetVehicleInFrontPlayer();
            if (veh != null && veh.LockStatus == VehicleLockStatus.Unlocked)
            {
                veh.LockStatus = VehicleLockStatus.CannotEnter;
                StorageController.SaveVehicle(new Kendaraan(veh));
                vehicles.Add(veh);

                blips.Add(BlipController.CreateBlip(veh));
                Notification.Show("Car Locked");
            }
        }
        public static void Unlock(List<Vehicle> vehicles, List<Blip> blips)
        {
            try
            {
                var veh = Helper.GetVehicleInFrontPlayer();
                if (veh != null && veh.LockStatus != VehicleLockStatus.Unlocked)
                {
                    int i = vehicles.FindIndex(x => x.DisplayName == veh.DisplayName && x.Mods.LicensePlate == veh.Mods.LicensePlate);
                    if (i >= 0)
                    {
                        vehicles.RemoveAt(i);
                    }
                    StorageController.DeleteVehicle(new Kendaraan(veh));
                    veh.LockStatus = VehicleLockStatus.Unlocked;

                    i = blips.FindIndex(x => x.Name == $"{veh.DisplayName}-{veh.Mods.LicensePlate}"); if (i >= 0)
                    {
                        blips[i].Delete();
                    }
                    Notification.Show("Car Unlocked");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Vehicle> LoadAllVehicle()
        {
            var kendaraans = StorageController.LoadAllKendaraan();
            if (kendaraans.Count > 0)
            {
                foreach (var _veh in kendaraans)
                {
                    var veh = _veh.Spawn();
                    veh.LockStatus = VehicleLockStatus.CannotEnter;
                    yield return veh;
                }
            }
        }
        public static void LoadAllVehicle(List<Vehicle> vehicles, List<Blip> blips)
        {
            try
            {
                vehicles.Clear();
                var kendaraans = StorageController.LoadAllKendaraan();
                if (kendaraans.Count > 0)
                {
                    foreach (var _veh in kendaraans)
                    {
                        var spawnVeh = _veh.Spawn();
                        spawnVeh.LockStatus = VehicleLockStatus.CannotEnter;
                        vehicles.Add(spawnVeh);
                    }

                    Notification.Show($"~b~{kendaraans.Count} ~w~Cars Has Been ~g~Spawned");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Reload(List<Vehicle> vehicles, List<Blip> blips)
        {
            if (vehicles.Count > 0)
            {
                foreach (var _veh in vehicles)
                {
                    _veh.Delete();
                }
                vehicles.Clear();
                //Dispose(vehicles);
                LoadAllVehicle(vehicles, blips);
            }
        }
        public static void Dispose(List<Vehicle> vehicles)
        {
            try
            {
                if (vehicles.Count > 0)
                {
                    foreach (var _veh in vehicles)
                    {
                        _veh.Delete();
                    }

                    vehicles.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
