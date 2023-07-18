using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieptidiCarControll_SHVDN.Models;
using DieptidiCarControll_SHVDN.Controller;
using GTA;
using GTA.Native;
using GTA.Math;
using GTA.UI;

namespace DieptidiCarControll_SHVDN.Controller
{
    static class BlipController
    {
        public static Blip CreateBlip(Kendaraan kendaraan)
        {
            Blip blip = World.CreateBlip(kendaraan.Position.ToVector3());
            blip.Sprite = BlipSprite.SportsCar;
            blip.Name = $"{kendaraan.ModelName}-{kendaraan.LicensePlate}";
            blip.Color = BlipColor.Blue;

            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, blip, true);

            return blip;
        }
        public static Blip CreateBlip(Vehicle vehicle)
        {
            Blip blip = World.CreateBlip(vehicle.Position);
            blip.Sprite = BlipSprite.SportsCar;
            blip.Name = $"{vehicle.DisplayName}-{vehicle.Mods.LicensePlate}";
            blip.Color = BlipColor.Blue;

            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, blip, true);

            return blip;
        }

        public static IEnumerable<Blip> RenderBlips(List<Kendaraan> kendaraans)
        {
            for (int i = 0; i < kendaraans.Count; i++)
            {
                yield return (CreateBlip(kendaraans[i]));
            }
        }

        public static IEnumerable<Blip> RenderBlips(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                yield return (CreateBlip(vehicles[i]));
            }
        }

        public static void DisposeBlips(List<Blip> blips)
        {
            for (int i = 0; i < blips.Count; i++)
            {
                try
                {
                    blips[i].Delete();
                }
                catch (Exception e)
                {
                    Notification.Show("~r~ERROR-BLIP! ~s~" + e.Message);
                }
            }
        }
    }
}
