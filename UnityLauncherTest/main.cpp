#include <QtCore/QCoreApplication>

#include "UnityLauncherCore.h"

#include <QDir>
#include <QDebug>
#include <Windows.h>
#include <iostream>

#pragma comment(lib, "UnityLauncherCore.lib")

int main(int argc, char *argv[])
{
	QCoreApplication a(argc, argv);

	UnityLauncherCore core;
	core.GetOpenedProjects();

	return a.exec();
}
												  
