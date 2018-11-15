#pragma once

#include <QtWidgets/QDialog>
#include <QListWidgetItem>
#include "ui_ProjectWidgetItem.h"
#include <UnityLauncherCore.h>


class ProjectWidgetItem : public QWidget
{
	Q_OBJECT

public:
	ProjectWidgetItem(UnityLauncherCore::ProjectInfo proj);
	const UnityLauncherCore::ProjectInfo& GetProjectInfo();

private:
	Ui::Form ui;
	UnityLauncherCore::ProjectInfo proj;
};
