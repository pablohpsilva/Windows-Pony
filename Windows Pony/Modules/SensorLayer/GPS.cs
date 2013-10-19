using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device;
using System.Device.Location;
using Windows.Devices.Geolocation;
using Windows_Pony.Modules;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using Windows_Pony.Modules.DataAccessLayer;

namespace Windows_Pony.Modules.SensorLayer
{
    public class GPS : Module
    {
        private GeoCoordinateWatcher watcher;
        private TextBlock notification;
        
        public GPS()
        {
            //if((bool) IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
            CheckStatus();
        }

        #region getterSetter
        public TextBlock TextBlock
        {
            get { return notification; }
            set { notification = value; }
        }

        public void SetNotificationList(TextBlock tb)
        {
            this.TextBlock = tb;
        }
        #endregion

        public void CheckStatus()
        {
            try
            {
                if (watcher == null)
                {
                    watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    watcher.MovementThreshold = 5;
                    watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(GeoCoordinateWatcher_StatusChanged);
                    watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(GeoCoordinateWatcher_PositionChanged);
                }
                watcher.Start();
            }
            catch (Exception except)
            {
                throw new Exception(except.ToString());
            }
        }

        void GeoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    this.TextBlock.Text = "Location Service is not enabled on the device";
                    break;

                case GeoPositionStatus.NoData:
                    this.TextBlock.Text = "The Location Service is working, but it cannot get location data";
                    break;
            }
        }

        void GeoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e.Position.Location.IsUnknown)
            {
                this.notification.Text = "Please wait while your prosition is determined....";
                return;
            }

            DataManager datamanager = (DataManager)ModuleManagerFactoryPool.getModule("data");
            datamanager.StartAddData("GPS", "Latitude: " + e.Position.Location.Latitude.ToString());
            datamanager.StartAddData("GPS", "Longitude: " + e.Position.Location.Longitude.ToString());
            datamanager.StartAddData("GPS", "Accuracy: " + e.Position.Location.HorizontalAccuracy.ToString());
            
            this.notification.Text = "Everything went OK.";
        }

    }
}
