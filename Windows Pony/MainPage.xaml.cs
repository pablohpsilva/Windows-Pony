using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows_Pony.Modules;
using Windows_Pony.Modules.BusinessLayer;
using Windows_Pony.Modules.SensorLayer;
using Windows_Pony.Modules.DataAccessLayer;

namespace Windows_Pony
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
            SendEmail email = (SendEmail)ModuleManagerFactoryPool.getModule("email");
            email.SetUserInformation(pwdInformationEmail.Password, pwdInformationPassword.Password);
            email.SetRegularEmailServer();
        }

        private void testEmailButton_Click(object sender, RoutedEventArgs e)
        {
            SendEmail email = (SendEmail)ModuleManagerFactoryPool.getModule("email");
            email.Send();
        }

        private void testLocationButton_Click(object sender, RoutedEventArgs e)
        {
            GPS l = (GPS)ModuleManagerFactoryPool.getModule("location");
            l.SetNotificationList(notification);
            //l.CheckStatus();
        }

        private void tglSetupKeyboard_Checked_1(object sender, RoutedEventArgs e)
        {
            //IsolatedFilesManager ifm = (IsolatedFilesManager)ModuleManagerFactoryPool.getModule("file");
            //MessageBox.Show(ifm.write());
            //MessageBox.Show(ifm.read());
        }

        private void testDataManagerButton_Click(object sender, RoutedEventArgs e)
        {
            DataManager dm = (DataManager)ModuleManagerFactoryPool.getModule("data");
            dm.StartAddData("text", "Hi! How are you?");
            dm.StartAddData("text", "que linda... Amo tanto!");
            dm.CloseSend();
        }

        private void testStorageButton_Click(object sender, RoutedEventArgs e)
        {
            IsolatedFilesManager ifm = (IsolatedFilesManager)ModuleManagerFactoryPool.getModule("file");
            ifm.write("Heloooooooh!");
            notification.Text += ifm.read().ToString();
            ifm.ResetFile();
            notification.Text += ifm.read().ToString();
        }
    }
}