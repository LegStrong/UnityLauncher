using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityLancher
{
    class UnityLancherCore
    {
        public Dictionary<string, string> versionDict = new Dictionary<string, string>();
        public Dictionary<string, string> recentProject = new Dictionary<string, string>();

        public UnityLancherCore()
        {
            Reset();
        }

        public void Reset()
        {
            InitUnityEditorInfo();
            InitRecentProjects();
        }

        public void InitUnityEditorInfo()
        {
            versionDict.Clear();

            var startPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs";
            var dirs = System.IO.Directory.GetDirectories(startPath, "Unity*(64-bit)", System.IO.SearchOption.TopDirectoryOnly);

            foreach (var dir in dirs)
            {
                var name = System.IO.Path.GetFileName(dir);
                var splits = name.Split(new char[] { ' ' });
                if (splits.Length >= 2)
                {
                    var links = System.IO.Directory.GetFiles(dir, "Unity*.lnk", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (var link in links)
                    {
                        var linkName = System.IO.Path.GetFileName(link);

                        string p = Shell.ResolveShortcut(link);
                        if (System.IO.Path.GetFileName(p) == "Unity.exe")
                        {
                            versionDict.Add(splits[1], link);
                            break;
                        }
                    }
                }
            }
        }

        public void InitRecentProjects()
        {
            recentProject.Clear();

            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey("SOFTWARE\\Unity Technologies\\Unity Editor 5.x");

            if (key != null)
            {
                foreach (var name in key.GetValueNames())
                {
                    if (name.StartsWith("RecentlyUsedProjectPaths-"))
                    {
                        var bs = key.GetValue(name) as byte[];
                        var str = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length - 1);
                        var version = GetVersion(str);
                        if (string.IsNullOrEmpty(version)) continue;

                        recentProject.Add(str, version);
                    }
                }
                key.Close();
            }
        }

        private string GetVersion(string path)
        {
            string ret = string.Empty;
            if (System.IO.Directory.Exists(path))
            {
                var versionfilePath = System.IO.Path.Combine(path, @"ProjectSettings\ProjectVersion.txt");
                if (System.IO.File.Exists(versionfilePath))
                {
                    var lines = System.IO.File.ReadAllLines(versionfilePath);
                    if (lines.Length > 0 && lines[0].StartsWith("m_EditorVersion: "))
                    {
                        ret = lines[0].Substring("m_EditorVersion: ".Length);
                    }
                }
            }

            return ret;
        }

        public void OpenProject(string path, string version)
        {
            if (versionDict.ContainsKey(version))
            {
                var args = string.Format("-projectPath \"{0}\"", path);
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(versionDict[version], args);
            }
        }
    }
}
