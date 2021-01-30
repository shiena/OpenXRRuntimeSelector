// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons
{
    public class VarjoRuntimeJson : IOpenXRRuntimeJson
    {
        private const string VarjoRuntimeKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Varjo\Runtime";
        private const string InstallDirKey = "InstallDir";
        private const string JsonName = @"varjo-openxr/VarjoOpenXR.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.Varjo;

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var varjoPathValue = Registry.GetValue(VarjoRuntimeKey, InstallDirKey, string.Empty);
            if (!(varjoPathValue is string vivePath))
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