using System.Configuration;

namespace EazyE2E.Console
{
    class Config
    {
        public int DoubleClickGap
        {
            get
            {
                string setting = ConfigurationManager.AppSettings["DoubleClickGap"];
                int value;
                return int.TryParse(setting, out value) ? value : 1000;
            }
        }
    }
}
