using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using UnityLancher.View;

namespace UnityLancher
{
    static class Program
    {
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
        static void Main()
        {
			bool isNewInstance;
			string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			Mutex mtx = new Mutex(true, appName, out isNewInstance);

			if (!isNewInstance)
			{
				Process[] myProcess = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(appName));
				if (null != myProcess.FirstOrDefault())
				{
					ShowWindow(myProcess.FirstOrDefault().MainWindowHandle, 1);
				}

				MessageBox.Show("UnityLancher is already running.");
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
        }
    }
}

