// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons.Internal
{
    internal class SteamVRRuntimeJson : IOpenXRRuntimeJson
    {
        private const string SteamPathKey = @"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam";
        private const string SteamPathValue = "SteamPath";
        private const string LibraryFoldersVdf = "steamapps/libraryfolders.vdf";
        private const string LibraryFoldersKey = "\"LibraryFolders\"";
        private const string JsonName = @"steamapps/common/SteamVR/steamxr_win64.json";
        private static readonly Regex RxKeyValue = new Regex(@"^[0-9]+$");

        public OpenXRRuntimeType Name => OpenXRRuntimeType.SteamVR;

        public bool TryGetJsonPath(out string jsonPath)
        {
            jsonPath = default;
            var steamPathValue = Registry.GetValue(SteamPathKey, SteamPathValue, string.Empty);
            if (!(steamPathValue is string steamPath))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(steamPath))
            {
                return false;
            }

            var libraryFolders = Path.Combine(steamPath, LibraryFoldersVdf);
            if (!File.Exists(libraryFolders))
            {
                return false;
            }

            var folders = new List<string> {steamPath};
            using (var sr = new StreamReader(libraryFolders))
            {
                var line = sr.ReadLine()?.Trim();
                if (string.Compare(line, LibraryFoldersKey, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    return false;
                }

                line = sr.ReadLine()?.Trim();
                if (line != "{")
                {
                    return false;
                }

                line = sr.ReadLine()?.Trim();
                while (line != null && line != "{")
                {
                    var m = RxKeyValue.Match(line);
                    if (m.Success)
                    {
                        folders.Add(m.Groups[1].Value.Replace(@"\\", @"\"));
                    }

                    line = sr.ReadLine()?.Trim();
                }
            }

            foreach (var folder in folders)
            {
                var path = Path.Combine(folder, JsonName);
                if (File.Exists(path))
                {
                    jsonPath = Path.GetFullPath(path);
                    return true;
                }
            }

            return false;
        }
    }
}