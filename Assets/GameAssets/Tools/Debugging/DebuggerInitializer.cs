using AdvancedDebugger;
using Common;
using DialogBoxService;
using Networking;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Debugging
{
    public static class DebuggerInitializer
    {
        private const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss.ffff";

        public static void Initialize()
        {
            Debugger.EnableLogWriting = true;
            Debugger.EnableMarkupFormat = true;
            Debugger.Initialize(GetDebuggerLogTypes(), GetColorizations(), Debug.Log, GetLogPath(), DateTimeFormat);
        }

        private static List<DebuggerLogType> GetDebuggerLogTypes()
        {
            return new List<DebuggerLogType>()
            {
                new DebuggerLogType(DebuggerLog.InfoDebug, Debug.Log, "#FFFFFF", false),
                new DebuggerLogType(DebuggerLog.Debug, Debug.Log, "#BDC7F0", true),
                new DebuggerLogType(DebuggerLog.InfoWarning, Debug.LogWarning, "#FFFFFF", false),
                new DebuggerLogType(DebuggerLog.Warning, Debug.LogWarning, "#FF5500", true),
                new DebuggerLogType(DebuggerLog.Error, Debug.LogError, "#FF0000", true),
            };
        }

        private static List<DebuggerColorization> GetColorizations()
        {
            return new List<DebuggerColorization>()
            {
                new DebuggerColorization(nameof(DataService), Color.green),
                new DebuggerColorization(nameof(DialogService), Color.yellow),
                new DebuggerColorization(nameof(ServerConnector), Color.cyan),
            };
        }

        private static string GetLogPath()
        {
            return $@"{Application.persistentDataPath}/Logs/Logs.txt";
        }
    }
}