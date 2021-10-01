import os
import threading

def sysinfo():
  threading.Timer(15.0, sysinfo).start()
  os.system('cmd /k "systeminfo | find "System Boot Time" & typeperf -sc 1 "\Processor(_Total)\% Processor Time" & wmic cpu get loadpercentage & typeperf -sc 1 "\Memory\Available Bytes" & typeperf -sc 1 "\LogicalDisk(_Total)\Disk Bytes/sec" & ping google.com"')
sysinfo()