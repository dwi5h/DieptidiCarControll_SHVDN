using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieptidiUtility_SHVDN;
using DieptidiCarControll_SHVDN.Controller;
using DieptidiBaseMenu_SHVDN;
using GTA;
using GTA.Math;
using GTA.UI;
using NativeUI;

namespace DieptidiCarControll_SHVDN
{
    public class Main : DieptidiBaseMenu
    {
        bool textOn = false;
        static List<Vehicle> vehicles;
        static List<Blip> blips;

        public Main() : base()
        {
            try
            {
                StorageController.CreateDirectory();
                vehicles = VehicleLockSystemController.LoadAllVehicle().ToList();
                blips = BlipController.RenderBlips(vehicles).ToList();

                Notification.Show("Car Controll ~g~Reloaded");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Action<UIMenuItem, UIMenu> OnItemSelect => (_item, _menu) =>
        {
            switch (_item.Text)
            {
                case "Lock":
                    VehicleLockSystemController.Lock(vehicles, blips);
                    _menu.Visible = false;
                    break;
                case "Unlock":
                    VehicleLockSystemController.Unlock(vehicles, blips);
                    _menu.Visible = false;
                    break;
                case "Reload":
                    VehicleLockSystemController.Reload(vehicles, blips);
                    _menu.Visible = false;
                    break;
                default:
                    break;
            }
        };

        public override Action Aborted => () =>
        {
            VehicleLockSystemController.Dispose(vehicles);
            BlipController.DisposeBlips(blips);
        };

        public override Action Tick => null;

        public override void BuildItem()
        {
            MenuItems.Add(new UIMenuItem("Lock"));
            MenuItems.Add(new UIMenuItem("Unlock"));
            MenuItems.Add(new UIMenuItem("Reload"));
        }
    }
}
