#pragma once

#include <QtWidgets/QDialog>
#include "ui_MainWindow.h"

class MainWindow : public QDialog
{
	Q_OBJECT

public:
	MainWindow(QWidget *parent = Q_NULLPTR);

private:
	Ui::MainWindowClass ui;
};
