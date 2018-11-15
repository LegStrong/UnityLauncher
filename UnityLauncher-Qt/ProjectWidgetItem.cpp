#include "ProjectWidgetItem.h"

ProjectWidgetItem::ProjectWidgetItem(UnityLauncherCore::ProjectInfo proj)
{
	ui.setupUi(this);

	this->proj = proj;

	ui.label_ProjectPath->setText(proj.projectPath);
	
	//ui->
}

const UnityLauncherCore::ProjectInfo & ProjectWidgetItem::GetProjectInfo()
{
	return proj;
}
