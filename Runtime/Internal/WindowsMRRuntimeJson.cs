// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.IO;

namespace OpenXRRuntimeJsons.Internal
{
    internal class WindowsMRRuntimeJson : IOpenXRRuntimeJson
    {
        private const string JsonName = "MixedRealityRuntime.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.WindowsMR;

        public bool TryGetJsonPath(out string jsonPath)
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var path = Path.Combine(systemPath, JsonName);
            if (File.Exists(path))
            {
                jsonPath = Path.GetFullPath(path);
                return true;
            }

            jsonPath = default;
            return false;
        }
    }
}