#pragma once

#include <Windows.h>
#include <string>
#include <list>

HRESULT ResolveIt(HWND hwnd, LPCSTR lpszLinkFile, LPWSTR lpszPath, int iPathBufferSize);                                                  

WCHAR*  QueryWorkingDirectory(DWORD pid);
WCHAR*  QueryWorkingDirectory(HANDLE pHandle);
//std::list<HANDLE> GetRunningProcessList
