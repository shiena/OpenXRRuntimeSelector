using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons
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

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var steamPathValue = Registry.GetValue(SteamPathKey, SteamPathValue, string.Empty);
            if (!(steamPathValue is string steamPath))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(steamPath))
            {
                return string.Empty;
            }

            var libraryFolders = Path.Combine(steamPath, LibraryFoldersVdf);
            if (!File.Exists(libraryFolders))
            {
                return string.Empty;
            }

            var folders = new List<string> {steamPath};
            using (var sr = new StreamReader(libraryFolders))
            {
                if (sr.ReadLine()?.Trim() != LibraryFoldersKey)
                {
                    return string.Empty;
                }

                if (sr.ReadLine()?.Trim() != "{")
                {
                    return string.Empty;
                }

                var line = sr.ReadLine()?.Trim();
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
                    return Path.GetFullPath(path);
                }
            }

            return string.Empty;
        }
    }
}