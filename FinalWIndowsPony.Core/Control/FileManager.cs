using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWindowsPony.Core.Control
{
    public class FileManager
    {
        private string FILE = "WindowsPhoneUserInfo.txt";
        private string PATH = Directory.GetCurrentDirectory();
        private string PATHFILE;

        public FileManager()
        {
            this.PATHFILE = this.PATH + @"\" + this.FILE;
            if (!File.Exists(this.PATHFILE))
                using (FileStream fs = new FileStream(this.PATHFILE, FileMode.CreateNew))
                    fs.Close();
        }

        public void CheckStatus() { }

        public string test()
        {
            return this.PATHFILE;
        }

        public void Write(string input)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.PATHFILE, true))
                {
                    writer.Write(input + ";");
                    writer.Close();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error while writing... \n" + exception.ToString());
            }
        }

        public string Read()
        {
            try
            {
                using (StreamReader reader = new StreamReader(this.PATHFILE))
                {
                    string aux = reader.ReadToEnd();
                    reader.Close();
                    return aux;
                }
            }
            catch (FileLoadException fileException)
            {
                throw fileException;
            }
            catch (FileNotFoundException fileNotException)
            {
                throw fileNotException;
            }
        }

        public string[] ReadFile()
        {
            return this.Read().Split(';');
        }
    }
}
