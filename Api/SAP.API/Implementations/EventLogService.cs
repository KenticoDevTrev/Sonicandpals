using CMS;
using CMS.Core;
using SAP.API.Implementations;

//[assembly: RegisterImplementation(typeof(IEventLogService), typeof(CustomEventLogService), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]

namespace SAP.API.Implementations
{
    public class CustomEventLogService : IEventLogService
    {
        public void LogEvent(EventLogData eventLogData)
        {
            //Service.Resolve<IEventLogWriter>().WriteLog(eventLogData);
        }
    }
}
