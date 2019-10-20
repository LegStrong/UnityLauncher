using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace UnityLancher
{
    class WinHelper
    {
        private static readonly string ExecutablePath = Assembly.GetEntryAssembly().Location;
        private static string Key = "UnityLancher";

        public static Process GetProcessUsedFile(string fileName)
        {
            var ps = Process.GetProcessesByName("Unity");
            fileName = fileName.Replace('/', '\\');
            var d = new Dictionary<string, string>();
            //处理软链接文件夹
            if (System.IO.File.Exists("config.txt"))
            {
                var ls = System.IO.File.ReadAllLines("config.txt");
                foreach (var l in ls)
                {
                    if (!l.StartsWith("#") && l.Contains("\t"))
                    {
                        var s = l.Split(new char[] { '\t' });
                        if (s.Length == 2)
                        {
                            d.Add(s[0], s[1]);
                        }
                    }
                }
            }

            foreach (var kvp in d)
            {
                fileName = fileName.Replace(kvp.Key, kvp.Value);
            }

            foreach (var p in ps)
            {
                ProcessStartInfo info = new ProcessStartInfo("handle", "-p " + p.Id);
                info.RedirectStandardOutput = true;
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                var hp = Process.Start(info);
                var output = hp.StandardOutput.ReadToEnd();
                if (output.Contains(fileName))
                {
                    return p;
                }
            }

            return null;
        }

        public static bool IsStartOnBoot()
        {
            var ret = false;
            Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (Rkey != null)
            {
                var value = Rkey.GetValue(Key, null);

                if (value != null && value as string == ExecutablePath)
                {
                    ret = true;
                }
            }
			Console.WriteLine("IsStartOnBoot: " + ret);
			return ret;
        }

        public static void SetStartOnBoot(bool startup)
        {
			Console.WriteLine("SetStartOnBoot: " + startup);
            Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (Rkey == null)
            {
                Rkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }

            if (startup)
            {
                Rkey.SetValue(Key, ExecutablePath);
            }
            else
            {
                Rkey.DeleteValue(Key);
            }
        }
    }
}
