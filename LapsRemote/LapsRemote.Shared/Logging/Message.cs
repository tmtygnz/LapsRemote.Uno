using System;
using System.Collections.Generic;
using System.Text;

namespace LapsRemote.Logging
{
    struct Message
    {
        public string LogMessage;
        public LogFrom LoggingFrom;
        public Level LogLevel;
        public DateTime LogTime;
    }
}
