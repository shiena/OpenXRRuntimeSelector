using System;
using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons
{
    internal class SystemDefaultRuntimeJson : IOpenXRRuntimeJson
    {
        private const string OpenXRMajorApiVersion = "1";
        private const string OpenXRKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Khronos\OpenXR\" + OpenXRMajorApiVersion;
        private const string OpenXRValue = "ActiveRuntime";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.SystemDefault;

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var pathValue = Registry.GetValue(OpenXRKey, OpenXRValue, string.Empty);
            if (pathValue is string path && !string.IsNullOrWhiteSpace(path))
            {
                if (File.Exists(path))
                {
                    return Path.GetFullPath(path);
                }
            }

            return string.Empty;
        }
    }
}