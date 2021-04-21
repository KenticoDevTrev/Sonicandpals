using CMS;
using CMS.Core;
using CMS.EventLog;
using SAP.API.Implementations;

//[assembly: RegisterImplementation(typeof(IEventLogWriter), typeof(EventLogWriter), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]

namespace SAP.API.Implementations
{
    public class EventLogWriter : IEventLogWriter
    {
        public void WriteLog(EventLogData eventLogData)
        {
            /*var eventType = EventType.FromEventTypeEnum(eventLogData.EventType);

            var info = new EventLogInfo(eventType, eventLogData.Source, eventLogData.EventCode)
            {
                EventDescription = eventLogData.EventDescription,
                EventUrl = eventLogData.EventUrl,
                UserID = eventLogData.UserID,
                UserName = eventLogData.UserName,
                NodeID = eventLogData.NodeID,
                DocumentName = eventLogData.DocumentName,
                IPAddress = eventLogData.IPAddress,
                SiteID = eventLogData.SiteID,
                EventMachineName = eventLogData.EventMachineName,
                EventUrlReferrer = eventLogData.EventUrlReferrer,
                EventUserAgent = eventLogData.EventUserAgent,
                EventTime = eventLogData.EventTime,
                Exception = eventLogData.Exception
            };

            EventLogProvider.LogEvent(info);*/

        }
    }
}
