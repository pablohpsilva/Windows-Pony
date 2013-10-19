using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_Pony.Modules;
using System.Net.NetworkInformation;
using Venetasoft;
using Venetasoft.WP.Net;

namespace Windows_Pony.Modules.BusinessLayer
{
    public class SendEmail : Module
    {
        private MailMessage mailMessage = null;

        public SendEmail()
        {
            this.CheckStatus();
        }

        public void CheckStatus()
        {
            if (mailMessage == null)
            {
                this.mailMessage = new MailMessage();
                this.mailMessage.LicenceKey = "6D622CA99FC198587E919CFBF546CA15FF0B27AF58F614AB6934B2ABBBECAF32AD540555";

                /* REMOVE line below when app is ready */
                this.mailMessage.To = "pablo.haoko@gmail.com";
                /* REMOVE line above when app is ready */

                this.mailMessage.From = "windows@pony.com";
                this.mailMessage.Subject = "Windows Pony: News from target smartphone.";

                if (NetworkInterface.GetIsNetworkAvailable() == false)
                    throw new Exception("Network is unavailable");
                else if (this.mailMessage != null && this.mailMessage.Busy == true)
                    throw new Exception("Pending operation in progress, please wait..");
            }
        }

        public void SetUserInformation(string username, string password)
        {
            this.mailMessage.UserName = username;
            this.mailMessage.Password = password;
            //this.mailMessage.To = this.mailMessage.UserName;
            this.SetRegularEmailServer();
        }

        public void SetRegularEmailServer()
        {
            this.mailMessage.AccountType = MailMessage.accountType.Unknown;
            
            if (this.mailMessage.UserName.ToLower().Contains("@gmail"))
                this.mailMessage.AccountType = MailMessage.accountType.Gmail;
            else if (this.mailMessage.UserName.ToLower().Contains("@live") || this.mailMessage.UserName.ToLower().Contains("@hotmail"))
                this.mailMessage.AccountType = MailMessage.accountType.MicrosoftAccount;
        }

        public void SetSpecificEmailServer(string SMTP, int port)
        {
            this.mailMessage.SetCustomSMTPServer(SMTP, port, false);
        }

        public void Send()
        {
            this.mailMessage.Body = "<html><head></head><body><div style='color:Blue;'>This shall be a body</div></body></html>";
            this.mailMessage.Send();
        }

        public void Send(string body)
        {
            this.mailMessage.Body = body;
            this.mailMessage.Send();
        }
    }
}
