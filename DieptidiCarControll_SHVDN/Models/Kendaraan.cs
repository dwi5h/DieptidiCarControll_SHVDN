using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;

namespace DieptidiCarControll_SHVDN.Models
{
    public class Kendaraan
    {
        public string ModelName { get; set; }
        public int ModelHash { get; set; }
        public SimpleVector3 Position { get; set; }
        public float Heading { get; set; }
        public string LicensePlate { get; set; }
        public LicensePlateStyle LicensePlateStyle { get; set; }
        public float DirtLevel { get; set; }
        public float BodyHealth { get; set; }
        public float EngineHealth { get; set; }
        public float PetrolTankHealth { get; set; }
        public bool CanTiresBurst { get; set; }
        public int Livery { get; set; }
        public float FuelLevel { get; set; }
        public VehicleClass Class { get; set; }
        public VehicleRoofState RoofState { get; set; }
        public VehicleWheelType WheelType { get; set; }
        public VehicleColor PrimaryColor { get; set; }
        public VehicleColor SecondaryColor { get; set; }
        public VehicleColor PearlescentColor { get; set; }
        public VehicleColor RimColor { get; set; }
        public VehicleColor TrimColor { get; set; }
        public VehicleWindowTint WindowTint { get; set; }
        public List<KendaraanMod> Mods { get; set; }

        public Kendaraan()
        {
            Mods = new List<KendaraanMod>();
        }

        public Kendaraan(Vehicle vehicle)
        {
            ModelHash = vehicle.Model.Hash;
            ModelName = vehicle.DisplayName;
            Position = new SimpleVector3(vehicle.Position);
            Heading = vehicle.Heading;
            WheelType = vehicle.Mods.WheelType;
            LicensePlate = vehicle.Mods.LicensePlate;
            LicensePlateStyle = vehicle.Mods.LicensePlateStyle;
            DirtLevel = vehicle.DirtLevel;
            BodyHealth = vehicle.BodyHealth;
            EngineHealth = vehicle.EngineHealth;
            PetrolTankHealth = vehicle.PetrolTankHealth;
            Livery = vehicle.Mods.Livery;
            FuelLevel = vehicle.FuelLevel;
            Class = vehicle.ClassType;
            RoofState = vehicle.RoofState;
            CanTiresBurst = vehicle.CanTiresBurst;
            PrimaryColor = vehicle.Mods.PrimaryColor;
            SecondaryColor = vehicle.Mods.SecondaryColor;
            PearlescentColor = vehicle.Mods.PearlescentColor;
            RimColor = vehicle.Mods.RimColor;
            TrimColor = vehicle.Mods.TrimColor;
            WindowTint = vehicle.Mods.WindowTint;

            Mods = vehicle.Mods.ToArray()
                .Select(mod => new KendaraanMod() { Type = mod.Type, Index = mod.Index, FriendlyName = mod.LocalizedTypeName })
                .ToList();
        }

        public Vehicle Spawn()
        {
            var spawnPosition = Position.ToVector3();
            var vehicle = World.CreateVehicle(ModelName, spawnPosition, Heading);

            if (vehicle == null)
            {
                vehicle = World.CreateVehicle(new Model(ModelHash), spawnPosition, Heading);
            }

            vehicle.DirtLevel = DirtLevel;
            vehicle.BodyHealth = BodyHealth;
            vehicle.EngineHealth = EngineHealth;
            vehicle.PetrolTankHealth = PetrolTankHealth;
            vehicle.Mods.Livery = Livery;
            vehicle.FuelLevel = FuelLevel;
            vehicle.RoofState = RoofState;
            vehicle.CanTiresBurst = CanTiresBurst;
            vehicle.Mods.WheelType = WheelType;
            vehicle.Mods.LicensePlate = LicensePlate;
            vehicle.Mods.LicensePlateStyle = LicensePlateStyle;
            vehicle.Mods.PrimaryColor = PrimaryColor;
            vehicle.Mods.SecondaryColor = SecondaryColor;
            vehicle.Mods.PearlescentColor = PearlescentColor;
            vehicle.Mods.RimColor = RimColor;
            vehicle.Mods.TrimColor = TrimColor;
            vehicle.Mods.WindowTint = WindowTint;

            GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_MOD_KIT, vehicle, 0);
            foreach (var myMod in Mods)
            {
                GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_MOD, vehicle, myMod.Type, myMod.Index, false);
            }

            return vehicle;
        }

        public Vehicle Spawn(float heading, Vector3? overridePosition = null)
        {
            var spawnPosition = overridePosition != null ? overridePosition.Value : Position.ToVector3();
            var vehicle = World.CreateVehicle(ModelName, spawnPosition, heading);

            if (vehicle == null)
            {
                vehicle = World.CreateVehicle(new Model(ModelHash), spawnPosition, heading);
            }

            vehicle.DirtLevel = DirtLevel;
            vehicle.BodyHealth = BodyHealth;
            vehicle.EngineHealth = EngineHealth;
            vehicle.PetrolTankHealth = PetrolTankHealth;
            vehicle.Mods.Livery = Livery;
            vehicle.FuelLevel = FuelLevel;
            vehicle.RoofState = RoofState;
            vehicle.CanTiresBurst = CanTiresBurst;
            vehicle.Mods.WheelType = WheelType;
            vehicle.Mods.LicensePlate = LicensePlate;
            vehicle.Mods.LicensePlateStyle = LicensePlateStyle;
            vehicle.Mods.PrimaryColor = PrimaryColor;
            vehicle.Mods.SecondaryColor = SecondaryColor;
            vehicle.Mods.PearlescentColor = PearlescentColor;
            vehicle.Mods.RimColor = RimColor;
            vehicle.Mods.TrimColor = TrimColor;
            vehicle.Mods.WindowTint = WindowTint;

            GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_MOD_KIT, vehicle, 0);
            foreach (var myMod in Mods)
            {
                GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_MOD, vehicle, myMod.Type, myMod.Index, false);
            }

            return vehicle;
        }
    }
}
