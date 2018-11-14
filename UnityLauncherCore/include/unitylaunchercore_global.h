#pragma once

#include <QtCore/qglobal.h>

#ifndef BUILD_STATIC
# if defined(UNITYLAUNCHERCORE_LIB)
#  define UNITYLAUNCHERCORE_EXPORT Q_DECL_EXPORT
# else
#  define UNITYLAUNCHERCORE_EXPORT Q_DECL_IMPORT
# endif
#else
# define UNITYLAUNCHERCORE_EXPORT
#endif
