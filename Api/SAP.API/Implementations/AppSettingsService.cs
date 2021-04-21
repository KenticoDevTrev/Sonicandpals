using CMS;
using CMS.Core;
using SAP.API.Implementations;
using System.Configuration;

/*[assembly: RegisterImplementation(typeof(IAppSettingsService), typeof(AppSettingsService), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]

namespace SAP.API.Implementations
{
    public class AppSettingsService : IAppSettingsService
    {
        public string this[string key]
        {
            get
            {
                return ConfigurationManager.AppSettings[key];
            }
            set
            {
            }
        }
    }
}
*/