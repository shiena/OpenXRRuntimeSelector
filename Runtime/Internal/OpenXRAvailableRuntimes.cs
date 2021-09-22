// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons.Internal
{
    internal class OpenXRAvailableRuntimes
    {
        /// <summary>
        /// <see cref="https://github.com/KhronosGroup/OpenXR-SDK-Source/blob/master/specification/loader/runtime.adoc?rgh-link-date=2021-09-19T07%3A53%3A42Z#runtime-enumeration"/>
        /// </summary>
        private const string OpenXRMajorApiVersion = "1";
        private const string OpenXRKey = @"SOFTWARE\Khronos\OpenXR\" + OpenXRMajorApiVersion + @"\AvailableRuntimes";
        private const int ActiveRuntimeValue = 0;

        public bool TryGetJsonPaths(out List<string> activeRuntimes)
        {
            using var activeRuntimesKey = Registry.LocalMachine.OpenSubKey(OpenXRKey);
            if (activeRuntimesKey == null)
            {
                activeRuntimes = null;
                return false;
            }

            activeRuntimes = activeRuntimesKey.GetValueNames()
                .Where(v => ActiveRuntimeValue.CompareTo(activeRuntimesKey.GetValue(v)) == 0)
                .ToList();

            return true;
        }
    }
}