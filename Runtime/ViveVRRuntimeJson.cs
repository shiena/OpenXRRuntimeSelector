using System;
using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons
{
    public class ViveVRRuntimeJson : IOpenXRRuntimeJson
    {
        private const string VivePathKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\HtcVive\Updater";
        private const string AppPathKey = "AppPath";
        private const string JsonName = @"App/ViveVRRuntime/ViveVR_openxr/ViveOpenXR.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.ViveVR;

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var vivePathValue = Registry.GetValue(VivePathKey, AppPathKey, string.Empty);
            if (!(vivePathValue is string vivePath))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(vivePath))
            {
                return string.Empty;
            }

            var jsonPath = Path.Combine(vivePath, JsonName);

            if (File.Exists(jsonPath))
            {
                return Path.GetFullPath(jsonPath);
            }

            return string.Empty;
        }
    }
}