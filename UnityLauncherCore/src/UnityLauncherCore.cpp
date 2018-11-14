#include "include/UnityLauncherCore.h"
#include "include/WinHelper.h"

#include <tlhelp32.h>
#include <tchar.h>
#include <QMap>
#include <QSettings>
#include <QDebug>
#include <QFile>
#include <QTextCodec>
#include <QDir>


UnityLauncherCore::UnityLauncherCore()
{
	Reset();
}


void UnityLauncherCore::InitUnityVersionInfo()
{
	versionDict.clear();

	QString startPath = "C:/ProgramData/Microsoft/Windows/Start Menu/Programs";
	QDir dir(startPath);
	QStringList filters;
	filters << "Unity*(64-bit)";
	QStringList dirs = dir.entryList(filters, QDir::Dirs);

	foreach(const QString& dir, dirs)
	{
		QDir d(startPath +"/" + dir);
		
		//QString name = d.dirName();
		QStringList splits = dir.split(" ", QString::SkipEmptyParts);
		if (splits.length() >= 2)
		{
			QStringList filter;
			filter << QString("Unity*.lnk");
			QStringList links = d.entryList(filter, QDir::Files);
			foreach(const QString& link, links)
			{
				//d.absoluteFilePath
				wchar_t targetPath[MAX_PATH];
				QString path = d.absoluteFilePath(link).replace("/", "\\");
				ResolveIt(NULL, d.absoluteFilePath(link).toStdString().data(), targetPath, MAX_PATH);
				
				//target = target.replace("PROGRA~1", "Program Files");
				wchar_t targetPathLong[MAX_PATH];
				GetLongPathName(targetPath, targetPathLong, MAX_PATH);
				QString target = QString::fromStdWString(std::wstring(targetPathLong));

				QFile f(target);
				if (f.exists() && target.endsWith("\\Unity.exe")) {
					QStringList sp = dir.split(QChar(' '));
					if (sp.length() >= 2 && sp[0] == "Unity") {
						versionDict.insert(sp[1], target);
					}
				}
			}
		}
	}
}

void UnityLauncherCore::InitRecentProjectsInfo()
{
	versionDict.clear();

	QSettings settings("HKEY_CURRENT_USER\\SOFTWARE\\Unity Technologies\\Unity Editor 5.x", QSettings::NativeFormat);
	QStringList allKeys = settings.allKeys();
	foreach(const QString &key, allKeys)
	{
		if (key.startsWith("RecentlyUsedProjectPaths-")) {
			QString s = settings.value(key).toString();
			//REG_BINARY  https://bugreports.qt.io/browse/QTBUG-98
			QByteArray data = QByteArray((const char*)s.utf16(), s.length() * 2);
			QString path = QString::fromUtf8(data);
			QDir dir;
			if (dir.exists(path)) {
				QString versionfilePath = path + "/ProjectSettings/ProjectVersion.txt";
				QFile file(versionfilePath);
				if (file.open(QFile::ReadOnly)) {
					QTextStream in(&file);
					while (!in.atEnd()) {
						QString line = in.readLine();
						QString tag = "m_EditorVersion:";
						if (line.startsWith(tag)) {
							QString version = line.mid(tag.length());
							versionDict.insert(key, version);
							break;
						}
					}
				}
			}
		}
	}
}

void UnityLauncherCore::Reset()
{
	InitUnityVersionInfo();
	InitRecentProjectsInfo();
}

QList<UnityLauncherCore::OpenedProject> UnityLauncherCore::GetOpenedProjects()
{
	QList<OpenedProject> list;

	// https://docs.microsoft.com/zh-cn/windows/desktop/ToolHelp/taking-a-snapshot-and-viewing-processes
	HANDLE hProcessSnap;
	PROCESSENTRY32 pe32;

	// Take a snapshot of all processes in the system.
	hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (hProcessSnap == INVALID_HANDLE_VALUE)
	{
	}

	// Set the size of the structure before using it.
	pe32.dwSize = sizeof(PROCESSENTRY32);

	// Retrieve information about the first process, and exit if unsuccessful
	if (!Process32First(hProcessSnap, &pe32))
	{
		CloseHandle(hProcessSnap);          // clean the snapshot object
		return list;
	}

	// Now walk the snapshot of processes, and
	do
	{
		std::wstring f(pe32.szExeFile);
		QString exeFile = QString::fromStdWString(f);
		if (exeFile == "Unity.exe") {
			//list << exeFile;

			WCHAR* path = QueryWorkingDirectory(pe32.th32ProcessID);
			OpenedProject info;
			info.pid = pe32.th32ProcessID;
			info.projectPath = QString::fromStdWString(path);
			list.append(info);
		}
	} while (Process32Next(hProcessSnap, &pe32));

	CloseHandle(hProcessSnap);

	return list;
}

