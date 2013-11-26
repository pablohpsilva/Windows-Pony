using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalWindowsPony.Core.Control;
using FinalWindowsPony.Core.Data;

namespace FinalWindowsPony.Core
{
    public class ModuleManagerFactoryPool
    {
        private static Dictionary<string, Object> pool = new Dictionary<string, Object>();

        public static void setObject(String key, Object value)
        {
            if (!pool.ContainsKey(key))
                pool.Add(key, value);
        }

        public static Object getObject(String key)
        {
            return pool[key];
        }

        public static GPSLocation getLocation()
        {
            if (!pool.ContainsKey("location"))
                setObject("location", new GPSLocation());
            return (GPSLocation)pool["location"];
        }

        public static SendEmail getEmail()
        {
            if (!pool.ContainsKey("email"))
                setObject("email", new SendEmail());
            return (SendEmail)pool["email"];
        }

        public static DataManager getDataManager()
        {
            if (!pool.ContainsKey("datamanager"))
                setObject("datamanager", new DataManager());
            return (DataManager)pool["datamanager"];
        }

        public static FileManager getFileManager()
        {
            if (!pool.ContainsKey("filemanager"))
                setObject("filemanager", new FileManager());
            return (FileManager)pool["filemanager"];
        }

        public static MyBackgroundWorker getBackgroundWorker()
        {
            if (!pool.ContainsKey("backgroundworker"))
                setObject("backgroundworker", new MyBackgroundWorker());
            return (MyBackgroundWorker)pool["backgroundworker"];
        }
    }
}
