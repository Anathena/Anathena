using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook
{
    public class HookManager
    {
        internal static List<Int32> HookedProcesses = new List<Int32>();
        internal static List<ProcessInfo> ProcessList = new List<ProcessInfo>();
        private static List<Int32> ActivePIDList = new List<Int32>();

        public static void AddHookedProcess(Int32 ProcessId)
        {
            lock (HookedProcesses)
            {
                HookedProcesses.Add(ProcessId);
            }
        }

        public static void RemoveHookedProcess(Int32 ProcessId)
        {
            lock (HookedProcesses)
            {
                HookedProcesses.Remove(ProcessId);
            }
        }

        public static Boolean IsHooked(Int32 ProcessId)
        {
            lock (HookedProcesses)
            {
                return HookedProcesses.Contains(ProcessId);
            }
        }

        [Serializable]
        public class ProcessInfo
        {
            public String FileName;
            public Int32 Id;
            public Boolean Is64Bit;
            public String User;
        }

        public static ProcessInfo[] EnumProcesses()
        {
            List<ProcessInfo> Result = new List<ProcessInfo>();

            foreach (Process Process in Process.GetProcesses())
            {
                try
                {
                    ProcessInfo ProcessInfo = new ProcessInfo();

                    ProcessInfo.FileName = Process.MainModule.FileName;
                    ProcessInfo.Id = Process.Id;
                    ProcessInfo.Is64Bit = RemoteHooking.IsX64Process(Process.Id);
                    ProcessInfo.User = RemoteHooking.GetProcessIdentity(Process.Id).Name;

                    Result.Add(ProcessInfo);
                }
                catch
                {

                }
            }

            return Result.ToArray();
        }

    } // End class

} // End namespace