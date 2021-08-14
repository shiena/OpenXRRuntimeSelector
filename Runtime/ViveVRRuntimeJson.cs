// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

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

        public bool TryGetJsonPath(out string jsonPath)
        {
            jsonPath = default;
            var vivePathValue = Registry.GetValue(VivePathKey, AppPathKey, string.Empty);
            if (!(vivePathValue is string vivePath))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(vivePath))
            {
                return false;
            }

            var path = Path.Combine(vivePath, JsonName);

            if (File.Exists(path))
            {
                jsonPath = Path.GetFullPath(path);
                return true;
            }

            return false;
        }
    }
}