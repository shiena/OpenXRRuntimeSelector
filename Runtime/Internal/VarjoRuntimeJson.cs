// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons.Internal
{
    public class VarjoRuntimeJson : IOpenXRRuntimeJson
    {
        private const string VarjoRuntimeKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Varjo\Runtime";
        private const string InstallDirKey = "InstallDir";
        private const string JsonName = @"varjo-openxr/VarjoOpenXR.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.Varjo;

        public bool TryGetJsonPath(out string jsonPath)
        {
            jsonPath = default;
            var varjoPathValue = Registry.GetValue(VarjoRuntimeKey, InstallDirKey, string.Empty);
            if (!(varjoPathValue is string vivePath))
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