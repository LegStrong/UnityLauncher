﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityLancher.Model;

namespace UnityLancher.Controller
{
	public class UnityProjectController
	{
		public Form mainForm;
		private UnityLancherCore model;

		public UnityProjectController()
		{
			model = new UnityLancherCore();
		}

		public Dictionary<string, string> GetProjects()
		{
			model.Reset();
			return model.recentProject;
		}

		public Dictionary<string, string> GetUnitys()
		{
			return model.versionDict;
		}

		public void OpenProject(string path, string version)
		{
			model.OpenProject(path, version);
		}

		public void OpenProject(string path)
		{
			var version = model.GetVersion(path);
			if (string.IsNullOrEmpty(version))
			{
			}
			else
			{
				model.OpenProject(path, version);
			}
		}

		public void CreateProject(string path, string version)
		{
			model.CreateProject(path, version);
		}

		public void HideForm()
        {
			mainForm.Visible = false;
        }
	}
}
