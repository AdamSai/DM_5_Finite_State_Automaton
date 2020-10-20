using System;
using System.Collections.Generic;
using System.Text;

namespace LogFiniteStateAutomaton
{
    class LogEntry
    {
        public string Level { get; }
        public int SystemId { get; }
        public int InstanceId { get; }
        public int ActionId { get; }
        public long TimeStamp { get; }

        public LogEntry(string level, int systemId, int instanceId, int actionId, long timeStamp)
        {
            Level = level;
            SystemId = systemId;
            InstanceId = instanceId;
            ActionId = actionId;
            TimeStamp = timeStamp;
        }

        public override string ToString()
        {
            return Level + " " + SystemId + " " + InstanceId + " " + ActionId + " " + TimeStamp;
        }
    }
}
