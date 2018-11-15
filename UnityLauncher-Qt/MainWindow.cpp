#include "MainWindow.h"
#include "ProjectWidgetItem.h"

#include <QPushButton>
#include <QProcess>

MainWindow::MainWindow(QWidget *parent)
	: QDialog(parent)
{
	ui.setupUi(this);

	foreach(UnityLauncherCore::ProjectInfo proj, core.GetProjectInfo())
	{
		ProjectWidgetItem *widget = new ProjectWidgetItem(proj);
		QListWidgetItem *listItem = new QListWidgetItem();

		listItem->setSizeHint(widget->sizeHint());

		ui.listWidget->addItem(listItem);
		ui.listWidget->setItemWidget(listItem, widget);

		connect(ui.listWidget, SIGNAL(itemClicked(QListWidgetItem*)),
			this, SLOT(onListMailItemClicked(QListWidgetItem*)));
	}
}

void MainWindow::onListMailItemClicked(QListWidgetItem* item)
{
	//ui.listWidget->setItemWidget
	ProjectWidgetItem *widget = (ProjectWidgetItem*)ui.listWidget->itemWidget(item);
	const UnityLauncherCore::ProjectInfo& proj = widget->GetProjectInfo();
	OpenProject(proj.version, QString("-projectPath ") + proj.projectPath);
}

void MainWindow::OpenProject(QString version, QString args)
{
	QMap<QString, QString> apps = core.GetUnityApps();
	QMap<QString, QString>::iterator iter = apps.find(version);
	if (iter != apps.end()) {
		QProcess p;
		p.start(*iter + args);
	}
}
