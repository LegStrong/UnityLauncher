#pragma once

#include "unitylaunchercore_global.h"
#include <QMap>
#include <QString>

class UNITYLAUNCHERCORE_EXPORT UnityLauncherCore
{
public:
	struct ProjectInfo
	{
		unsigned long pid;
		QString projectPath;
		QString version;

		inline bool operator==(const ProjectInfo& rhs) {
			return projectPath == rhs.projectPath;
		}

		inline ProjectInfo& operator=(const ProjectInfo& rhs) {
			projectPath = rhs.projectPath;
			pid = rhs.pid;
			version = rhs.version;
			return *this;
		}
	};

	UnityLauncherCore();
	void InitUnityVersionInfo();
	void InitRecentProjectsInfo();
	void Reset();
	QList<ProjectInfo> GetOpenedProjects();
	const QList<ProjectInfo>& GetProjectInfo();

	const QMap<QString, QString>& GetUnityApps();


private:
	QMap<QString, QString> versionDict;
	QList<ProjectInfo> recentProject;
};
												  
