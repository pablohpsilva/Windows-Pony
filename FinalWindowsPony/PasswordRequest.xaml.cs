using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FinalWindowsPony.Core;

namespace FinalWindowsPony
{
    public partial class PasswordRequest : PhoneApplicationPage
    {
        public PasswordRequest()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string password = ModuleManagerFactoryPool.getFileManager().ReadFile()[1];
            if (pwdSignIn.Password == "")
                MessageBox.Show("Please, insert a password");
            else if (pwdSignIn.Password != password)
                MessageBox.Show("Wrong Password. Try again.");
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            pwdSignIn.Focus();
        }
    }
}