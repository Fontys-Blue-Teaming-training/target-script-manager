using System;
using System.Collections.Generic;
using System.Text;

namespace TargetScriptManager
{
    public class SystemInformation
    {
        public string CurrentSystemUpTime { get; set; }
        public double CurrentCpuPercentageUsage { get; set; }
        public double CurrentRamPercentageUsage { get; set; }
        public bool CurrentInternetConnectivity { get; set; }
    }
}
