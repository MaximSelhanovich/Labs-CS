using System;
using System.Runtime.InteropServices;

namespace Lab4
{
    class Lab4
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetSystemInfo(out SYSTEM_INFO Info);

        public static void PrintProcessorArchitecture(ushort wProcessorArchitecture)
        {
            switch (wProcessorArchitecture)
            {
                case 0:
                    Console.WriteLine("Processor architecture: Intel x86");
                    break;
                case 6:
                    Console.WriteLine("Processor architecture: Intel x64" +
                                      "(Intel Itanium-based)");
                    break;
                case 9:
                    Console.WriteLine("Processor architecture: " +
                                      "(AMD or Intel) x64");
                    break;
                case 5:
                    Console.WriteLine("Processor architecture: ARM");
                    break;
                case 12:
                    Console.WriteLine("Processor architecture: ARM64");
                    break;
                default:
                    Console.WriteLine("Unknown processor architecture");
                    break;
            }
        }

        public static void PrintSystemInfo()
        {
            SYSTEM_INFO info;
            GetSystemInfo(out info);

            PrintProcessorArchitecture(info.processorArchitecture);
            Console.WriteLine("Page size: {0} KB", info.pageSize / 1024);
            Console.WriteLine("The number of logical processors " +
                              "in the current group: {0}", info.numberOfProcessors);
            Console.WriteLine("Processor type: {0}", info.processorType);
            Console.WriteLine("Allocation granularity: {0}", info.allocationGranularity);
            Console.WriteLine("Processor level: {0}", info.processorLevel);
            Console.WriteLine("Processor revision: {0}", info.processorRevision);

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("\tSome characteristics");

            PrintPhysicallyInstalledSystemMemory();

            PrintSystemInfo();

            PrintGlobalMemoryStatus();

            PrintDiskFreeSpace("C:\\");

            PrintDiskFreeSpace("D:\\");

            PrintSystemPowerStatus();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LPSYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte SystemStatusFlag;
            public int BatteryLifeTime;
            public int BatteryFullLifeTime;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetSystemPowerStatus(out LPSYSTEM_POWER_STATUS lpSystemPowerStatus);

        static void PrintSystemPowerStatus()
        {
            LPSYSTEM_POWER_STATUS lpSystemPowerStatus;
            GetSystemPowerStatus(out lpSystemPowerStatus);

            PrintACLineStatus(lpSystemPowerStatus.ACLineStatus);
            PrintBatteryFlag(lpSystemPowerStatus.BatteryFlag);
            PrintBatteryLifePercent(lpSystemPowerStatus.BatteryLifePercent);
            SystemStatusFlag(lpSystemPowerStatus.SystemStatusFlag);
            PrintLifeTime(lpSystemPowerStatus.BatteryLifeTime, "Remaining");
            PrintLifeTime(lpSystemPowerStatus.BatteryFullLifeTime, "Full battery");
        }

        static void PrintACLineStatus(byte ACLineStatus)
        {
            switch (ACLineStatus)
            {
                case 0:
                    Console.WriteLine("\nACLineStatus: Offline");
                    break;
                case 1:
                    Console.WriteLine("\nACLineStatus: Online");
                    break;
            }
        }

        static void PrintBatteryFlag(byte BatteryFlag)
        {
            Console.WriteLine(BatteryFlag);
            switch (BatteryFlag)
            {
                case 0:
                    Console.WriteLine("The battery is not being charged and " +
                                      "the battery capacity is between " +
                                      "low(< 33%) and high(> 66%)");
                    break;
                case 1:
                    Console.WriteLine("High — the battery capacity is " +
                                      "at more than 66 percent");
                    break;
                case 2:
                    Console.WriteLine("Low — the battery capacity is " +
                                      "at less than 33 percent");
                    break;
                case 4:
                    Console.WriteLine("Critical — the battery capacity is " +
                                      "at less than five percent");
                    break;
                case 8:
                    Console.WriteLine("Charging");
                    break;
                case 128:
                    Console.WriteLine("No system battery");
                    break;
                case 255:
                    Console.WriteLine("Unknown status — unable to read " +
                                      "the battery flag information");
                    break;
            }
        }

        static void PrintBatteryLifePercent(byte BatteryLifePercent)
        {
            if (BatteryLifePercent == 255)
                Console.WriteLine("Battery charge: status is unknown");
            else
                Console.WriteLine("Battery charge: {0}%", BatteryLifePercent);
        }

        static void SystemStatusFlag(byte SystemStatusFlag)
        {
            if (SystemStatusFlag == 0)
                Console.WriteLine("Battery saver is off");
            else
                Console.WriteLine("Battery saver on. " +
                                  "Save energy where possible");
        }

        static void PrintLifeTime(long LifeTime, string Action)
        {
            if (LifeTime == -1)
                Console.WriteLine(Action + " seconds are unknown " +
                                  "or the device is connected to AC power");
            else
                Console.WriteLine(Action + " time: {0}h {1}m",
                                  LifeTime / 3600, LifeTime / 60 % 60);
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out ulong TotalMemoryInKilobytes);

        static void PrintPhysicallyInstalledSystemMemory()
        {
            ulong memoryInKilobytes;
            GetPhysicallyInstalledSystemMemory(out memoryInKilobytes);
            Console.WriteLine(memoryInKilobytes / 1024 / 1024 +
                              " GB of RAM installed.\n");
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
                                              out ulong lpFreeBytesAvailable,
                                              out ulong lpTotalNumberOfBytes,
                                              out ulong lpTotalNumberOfFreeBytes);

        static void PrintDiskFreeSpace(string lpDirectoryName)
        {
            Console.WriteLine("\nInformation about \"{0}\"", lpDirectoryName);

            ulong FreeBytesAvailable;
            ulong TotalNumberOfBytes;
            ulong TotalNumberOfFreeBytes;

            GetDiskFreeSpaceEx(lpDirectoryName, out FreeBytesAvailable,
                                                out TotalNumberOfBytes,
                                                out TotalNumberOfFreeBytes);

            Console.WriteLine("Free gigabytes available: " +
                               FreeBytesAvailable / 1024 / 1024 / 1024);
            Console.WriteLine("Total number of gigabytes: " +
                               TotalNumberOfBytes / 1024 / 1024 / 1024);
            Console.WriteLine("Total number of free gigabytes: " +
                               TotalNumberOfFreeBytes / 1024 / 1024 / 1024);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern void GlobalMemoryStatusEx(out MEMORYSTATUSEX lpBuffer);

        public static void PrintGlobalMemoryStatus()
        {
            MEMORYSTATUSEX memStatus;
            memStatus.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(out memStatus);

            Console.WriteLine("There is {0} percent of memory in use.\n",
                               memStatus.dwMemoryLoad);
            Console.WriteLine("There are {0} total KB of physical memory.",
                               memStatus.ullTotalPhys / 1024);
            Console.WriteLine("There are {0} free  KB of physical memory.\n",
                               memStatus.ullAvailPhys / 1024);
            Console.WriteLine("There are {0} total KB of paging file.",
                               memStatus.ullTotalPageFile / 1024);
            Console.WriteLine("There are {0}  free  KB of paging file.\n",
                               memStatus.ullAvailPageFile / 1024);
            Console.WriteLine("There are {0} total KB of virtual memory.",
                               memStatus.ullTotalVirtual / 1024);
            Console.WriteLine("There are {0} free  KB of virtual memory.\n",
                               memStatus.ullAvailVirtual / 1024);
            Console.WriteLine("There are {0} free  KB of extended memory.",
                               memStatus.ullAvailExtendedVirtual / 1024);
        }
    }
}
