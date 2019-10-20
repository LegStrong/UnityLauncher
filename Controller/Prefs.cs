using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLancher.Controller
{
	public static class Prefs
	{
		static readonly string keyName = "SOFTWARE\\August\\UnityLancher";

		public static string GetString(string key)
		{
			Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);
			if (Rkey == null)
			{
				Rkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyName);
			}

			return Rkey.GetValue(key, "") as string;
		}

		public static void SetString(string key, string value)
		{
			Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);
			if (Rkey == null)
			{
				Rkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyName);
			}

			Rkey.SetValue(key, value);
		}

		public static string GetPrefProjectPath()
		{
			var path = GetString("ProjectPath");
			if (!string.IsNullOrEmpty(path))
			{
				return path;
			}

			return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		public static string GetPrefVersion()
		{
			return GetString("UnityVersion");
		}
	}
}
