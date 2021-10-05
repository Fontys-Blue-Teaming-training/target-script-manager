using System;
using System.Collections.Generic;
using System.Text;

namespace TargetScriptManager
{
    public class SystemInformation
    {
        public string CurrentBootTime { get; set; }
        public string CurrentTotalProcessorTime { get; set; }
        public string CurrentLoadPercentage { get; set; }
        public string CurrentAvailableMemoryBytes { get; set; }
        public string CurrentDiskBytesPerSec { get; set; }
        public string CurrentPingStatistics { get; set; }
    }
}
