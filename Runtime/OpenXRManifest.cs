// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using UnityEngine;

namespace OpenXRRuntimeJsons
{
    [Serializable]
    public class ManifestRuntime : ISerializationCallbackReceiver
    {
        [NonSerialized] public string Name;
        [NonSerialized] public string LibraryPath;
        [SerializeField] [HideInInspector] private string name;
        [SerializeField] [HideInInspector] private string library_path;

        public void OnBeforeSerialize()
        {
            name = Name;
            library_path = LibraryPath;
        }

        public void OnAfterDeserialize()
        {
            Name = name;
            LibraryPath = library_path;
        }
    }

    [Serializable]
    public class Manifest : ISerializationCallbackReceiver
    {
        [NonSerialized] public string FileFormatVersion;
        [NonSerialized] public ManifestRuntime Runtime;
        [SerializeField] [HideInInspector] private string file_format_version;
        [SerializeField] [HideInInspector] private ManifestRuntime runtime;

        public void OnBeforeSerialize()
        {
            file_format_version = FileFormatVersion;
            runtime = Runtime;
        }

        public void OnAfterDeserialize()
        {
            FileFormatVersion = file_format_version;
            Runtime = runtime;
        }

        public static Manifest FromJson(string jsonPath)
        {
            return JsonUtility.FromJson<Manifest>(jsonPath);
        }

        public static string ToJson(Manifest manifest)
        {
            return JsonUtility.ToJson(manifest);
        }
    }
}