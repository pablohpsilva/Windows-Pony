using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWindowsPony.Core.Data
{
    public class GPSLocation
    {
        private int METERSTRAVELED = 10;
        private GeoCoordinateWatcher watcher;

        public GPSLocation()
        {
            IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
            try
            {
                if (this.watcher == null)
                {
                    this.watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    this.watcher.MovementThreshold = METERSTRAVELED;
                    this.watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(GeoCoordinateWatcher_StatusChanged);
                    this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(GeoCoordinateWatcher_PositionChanged);
                }
                this.watcher.Start();
            }
            catch (Exception except)
            {
                throw new Exception(except.ToString());
            }
        }

        public void setMetersTraveled(int value)
        {
            if (value <= 0)
                this.METERSTRAVELED = 1;
        }

        void GeoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    ModuleManagerFactoryPool.getDataManager().StartAddData("GPS", "Location Service is not enabled on the device");
                    break;

                case GeoPositionStatus.NoData:
                    ModuleManagerFactoryPool.getDataManager().StartAddData("GPS", "The Location Service is working, but it cannot get location data");
                    break;
            }
        }

        void GeoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e.Position.Location.IsUnknown)
            {
                //toast.Content = "Please wait while your prosition is determined....";
                return;
            }

            string googleMaps = "https://maps.google.com/maps?f=q&source=s_q&hl=en&geocode=&q=" + e.Position.Location.Latitude.ToString() + ",+" + e.Position.Location.Longitude.ToString();

            ModuleManagerFactoryPool.getDataManager().StartAddData("GPS", googleMaps);
            ModuleManagerFactoryPool.getDataManager().StartAddData("GPS", "Accuracy: " + e.Position.Location.HorizontalAccuracy.ToString());
        }
    }
}
