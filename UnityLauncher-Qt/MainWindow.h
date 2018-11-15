#pragma once

#include <QtWidgets/QDialog>
#include "ui_MainWindow.h"
#include <UnityLauncherCore.h>

#pragma comment(lib, "UnityLauncherCore.lib")

class QListWidgetItem;

class MainWindow : public QDialog
{
	Q_OBJECT

public:
	MainWindow(QWidget *parent = Q_NULLPTR);
	void onListMailItemClicked(QListWidgetItem* item);


	void OpenProject(QString cmd, QString args);

private:
	Ui::MainWindowClass ui;
	UnityLauncherCore core;
};
