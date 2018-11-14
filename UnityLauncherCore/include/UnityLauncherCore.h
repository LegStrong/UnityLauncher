#pragma once

#include "unitylaunchercore_global.h"
#include <QMap>
#include <QString>

class UNITYLAUNCHERCORE_EXPORT UnityLauncherCore
{
public:
	struct OpenedProject
	{
		QString projectPath;
		unsigned long pid;
	};

	UnityLauncherCore();
	void InitUnityVersionInfo();
	void InitRecentProjectsInfo();
	void Reset();
	QList<OpenedProject> GetOpenedProjects();

private:
	QMap<QString, QString> versionDict;
	QMap<QString, QString> recentProject;
};
												  
