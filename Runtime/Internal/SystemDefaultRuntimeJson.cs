// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons.Internal
{
    internal class SystemDefaultRuntimeJson : IOpenXRRuntimeJson
    {
        private const string OpenXRMajorApiVersion = "1";
        private const string OpenXRKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Khronos\OpenXR\" + OpenXRMajorApiVersion;
        private const string OpenXRValue = "ActiveRuntime";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.SystemDefault;

        public bool TryGetJsonPath(out string jsonPath)
        {
            var pathValue = Registry.GetValue(OpenXRKey, OpenXRValue, string.Empty);
            if (pathValue is string path && !string.IsNullOrWhiteSpace(path))
            {
                if (File.Exists(path))
                {
                    jsonPath = Path.GetFullPath(path);
                    return true;
                }
            }

            jsonPath = default;
            return false;
        }
    }
}