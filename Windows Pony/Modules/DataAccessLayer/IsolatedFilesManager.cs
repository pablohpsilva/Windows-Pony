using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace Windows_Pony.Modules.DataAccessLayer
{
    public class IsolatedFilesManager : Module
    {
        private string fileName = "wp.html";
        private IsolatedStorageFile ISFile;

        public IsolatedFilesManager()
        {
            this.CheckStatus();
        }

        public void CheckStatus()
        {
            if (this.ISFile == null)
            {
                try
                {
                    this.ISFile = IsolatedStorageFile.GetUserStoreForApplication();
                    if (!this.ISFile.FileExists(this.fileName))
                        this.ISFile.CreateFile(this.fileName);
                }
                catch (Exception excep)
                {
                    throw new Exception("IsolatedFilesManager ERROR: " + excep.ToString());
                }
            }
        }

        public void write(string input)
        {
            try
            {
                using (IsolatedStorageFileStream isostream = new IsolatedStorageFileStream(this.fileName, FileMode.Append, this.ISFile))
                    using (StreamWriter writer = new StreamWriter(this.fileName))
                        writer.WriteLine(input);
            }
            catch (Exception excep)
            {
                throw new Exception("IsolatedFilesManager WRITE ERROR: " + excep.ToString());
            }
        }

        public string read()
        {
            try
            {
                using (IsolatedStorageFileStream isostream = new IsolatedStorageFileStream(this.fileName, FileMode.Open, this.ISFile))
                    using (StreamReader reader = new StreamReader(this.fileName))
                        return reader.ReadToEnd();
            }
            catch (Exception excep)
            {
                throw new Exception("IsolatedFilesManager WRITE ERROR: " + excep.ToString());
            }
        }

        public void ResetFile()
        {
            try
            {
                using (IsolatedStorageFileStream isostream = new IsolatedStorageFileStream(this.fileName, FileMode.Truncate, this.ISFile))
                    using (StreamWriter writer = new StreamWriter(this.fileName))
                        writer.WriteLine("o|o");
            }
            catch (Exception excep)
            {
                throw new Exception("IsolatedFilesManager WRITE ERROR: " + excep.ToString());
            }
        }
    }
}
