using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_Pony.Modules.DataAccessLayer;
using Windows_Pony.Modules.BusinessLayer;
using Windows_Pony.Modules.SensorLayer;

namespace Windows_Pony.Modules
{
    public abstract class ModuleManagerFactoryPool
    {
        private static Dictionary<string, Module> pool = new Dictionary<string, Module>();

        public static Module getModule(string name)
        {
            switch (name.ToLower())
            {
                case "location":
                    if (!pool.ContainsKey("location"))
                        pool.Add("location", new GPS());
                    return pool["location"];
                case "email":
                    if (!pool.ContainsKey("email"))
                        pool.Add("email", new SendEmail());
                    return pool["email"];
                case "file":
                    if (!pool.ContainsKey("file"))
                        pool.Add("file", new IsolatedFilesManager());
                    return pool["file"];
                case "data":
                    if (!pool.ContainsKey("data"))
                        pool.Add("data", new DataManager());
                    return pool["data"];
                default:
                    return null;
            }
        }
    }
}
