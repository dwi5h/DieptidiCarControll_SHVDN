using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieptidiUtility_SHVDN;
using DieptidiCarControll_SHVDN.Controller;
using GTA;
using GTA.Math;
using GTA.UI;
using NativeUI;

namespace DieptidiCarControll_SHVDN
{
    public class Main : Script
    {
        bool textOn = false;
        List<Vehicle> vehicles;
        List<Blip> blips;

        MenuPool menuPool;
        public UIMenu UImenuCarControll;
        UIMenuItem Lock, Unlock, Reload;

        public Main()
        {
            try
            {
                StorageController.CreateDirectory();
                vehicles = VehicleLockSystemController.LoadAllVehicle().ToList();
                blips = BlipController.RenderBlips(vehicles).ToList();

                Lock = new UIMenuItem("Lock");
                Unlock = new UIMenuItem("Unlock");
                Reload = new UIMenuItem("Reload");

                UImenuCarControll = new UIMenu("Car Controll", "");
                UImenuCarControll.MouseControlsEnabled = true;
                UImenuCarControll.AddItem(Lock);
                UImenuCarControll.AddItem(Unlock);
                UImenuCarControll.AddItem(Reload);

                UImenuCarControll.OnItemSelect += UImenuCarControll_OnItemSelect;

                menuPool = new MenuPool();
                menuPool.Add(UImenuCarControll);

                Tick += Main_Tick;
                KeyUp += Main_KeyUp;
                Aborted += Main_Aborted;

                Notification.Show("Car Controll ~g~Reloaded");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Main_Aborted(object sender, EventArgs e)
        {
            VehicleLockSystemController.Dispose(vehicles);
            BlipController.DisposeBlips(blips);
        }

        private void UImenuCarControll_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            switch (index)
            {
                case 0:
                    VehicleLockSystemController.Lock(vehicles, blips);
                    UImenuCarControll.Visible = false;
                    break;
                case 1:
                    VehicleLockSystemController.Unlock(vehicles, blips);
                    UImenuCarControll.Visible = false;
                    break;
                case 2:
                    VehicleLockSystemController.Reload(vehicles, blips);
                    UImenuCarControll.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                UImenuCarControll.Visible = true;
            }
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            menuPool.ProcessMenus();
            //if (textOn)
            //{
            //    Helper.Draw3dText(vehicle.Position, vehicle.EngineHealth.ToString());
            //}
        }
    }
}
