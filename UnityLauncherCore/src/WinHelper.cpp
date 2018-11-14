#include "include/WinHelper.h"
#include "windows.h"
#include "shobjidl.h"
#include "shlguid.h"
#include "strsafe.h"


// https://docs.microsoft.com/zh-cn/windows/desktop/shell/links
// ResolveIt - Uses the Shell's IShellLink and IPersistFile interfaces 
//             to retrieve the path and description from an existing shortcut. 
//
// Returns the result of calling the member functions of the interfaces. 
//
// Parameters:
// hwnd         - A handle to the parent window. The Shell uses this window to 
//                display a dialog box if it needs to prompt the user for more 
//                information while resolving the link.
// lpszLinkFile - Address of a buffer that contains the path of the link,
//                including the file name.
// lpszPath     - Address of a buffer that receives the path of the link
//					target, including the file name.
// lpszDesc     - Address of a buffer that receives the description of the 
//                Shell link, stored in the Comment field of the link
//                properties.
HRESULT ResolveIt(HWND hwnd, LPCSTR lpszLinkFile, LPWSTR lpszPath, int iPathBufferSize)
{
	HRESULT hres;
	IShellLink* psl;
	WCHAR szGotPath[MAX_PATH];
	WCHAR szDescription[MAX_PATH];
	WIN32_FIND_DATA wfd;

	*lpszPath = 0; // Assume failure 

				   // Get a pointer to the IShellLink interface. It is assumed that CoInitialize
				   // has already been called. 
	CoInitialize(NULL);
	hres = CoCreateInstance(CLSID_ShellLink, NULL, CLSCTX_INPROC_SERVER, IID_IShellLink, (LPVOID*)&psl);
	if (SUCCEEDED(hres))
	{
		IPersistFile* ppf;

		// Get a pointer to the IPersistFile interface. 
		hres = psl->QueryInterface(IID_IPersistFile, (void**)&ppf);

		if (SUCCEEDED(hres))
		{
			WCHAR wsz[MAX_PATH];

			// Ensure that the string is Unicode. 
			MultiByteToWideChar(CP_ACP, 0, lpszLinkFile, -1, wsz, MAX_PATH);

			// Add code here to check return value from MultiByteWideChar 
			// for success.

			// Load the shortcut. 
			hres = ppf->Load(wsz, STGM_READ);

			if (SUCCEEDED(hres))
			{
				// Resolve the link. 
				hres = psl->Resolve(hwnd, 0);

				if (SUCCEEDED(hres))
				{
					// Get the path to the link target. 
					hres = psl->GetPath(szGotPath, MAX_PATH, (WIN32_FIND_DATA*)&wfd, SLGP_SHORTPATH);

					if (SUCCEEDED(hres))
					{
						// Get the description of the target. 
						hres = psl->GetDescription(szDescription, MAX_PATH);

						if (SUCCEEDED(hres))
						{
							hres = StringCbCopy(lpszPath, iPathBufferSize, szGotPath);
							if (SUCCEEDED(hres))
							{
								// Handle success
							}
							else
							{
								// Handle the error
							}
						}
					}
				}
			}

			// Release the pointer to the IPersistFile interface. 
			ppf->Release();
		}

		// Release the pointer to the IShellLink interface. 
		psl->Release();
	}
	return hres;
}


typedef ULONG   PPS_POST_PROCESS_INIT_ROUTINE;

// NtQueryInformationProcess for pure 32 and 64-bit processes
typedef NTSTATUS(NTAPI *_NtQueryInformationProcess)(
	IN HANDLE ProcessHandle,
	ULONG ProcessInformationClass,
	OUT PVOID ProcessInformation,
	IN ULONG ProcessInformationLength,
	OUT PULONG ReturnLength OPTIONAL
	);

typedef struct _UNICODE_STRING {
	USHORT Length;
	USHORT MaximumLength;
	PWSTR  Buffer;
} UNICODE_STRING;

typedef struct _PEB_LDR_DATA {
	BYTE       Reserved1[8];
	PVOID      Reserved2[3];
	LIST_ENTRY InMemoryOrderModuleList;
} PEB_LDR_DATA, *PPEB_LDR_DATA;

typedef struct _RTL_USER_PROCESS_PARAMETERS {
	ULONG                   MaximumLength;
	ULONG                   Length;
	ULONG                   Flags;
	ULONG                   DebugFlags;
	PVOID                   ConsoleHandle;
	ULONG                   ConsoleFlags;
	HANDLE                  StdInputHandle;
	HANDLE                  StdOutputHandle;
	HANDLE                  StdErrorHandle;
	UNICODE_STRING          CurrentDirectoryPath;
	HANDLE                  CurrentDirectoryHandle;
	UNICODE_STRING          DllPath;
	UNICODE_STRING          ImagePathName;
	UNICODE_STRING          CommandLine;
	PVOID                   Environment;
	ULONG                   StartingPositionLeft;
	ULONG                   StartingPositionTop;
	ULONG                   Width;
	ULONG                   Height;
	ULONG                   CharWidth;
	ULONG                   CharHeight;
	ULONG                   ConsoleTextAttributes;
	ULONG                   WindowFlags;
	ULONG                   ShowWindowFlags;
	UNICODE_STRING          WindowTitle;
	UNICODE_STRING          DesktopName;
	UNICODE_STRING          ShellInfo;
	UNICODE_STRING          RuntimeData;
	//RTL_DRIVE_LETTER_CURDIR DLCurrentDirectory[0x20];

} RTL_USER_PROCESS_PARAMETERS, *PRTL_USER_PROCESS_PARAMETERS;

typedef struct _PEB {
	BYTE                          Reserved1[2];
	BYTE                          BeingDebugged;
	BYTE                          Reserved2[1];
	PVOID                         Reserved3[2];
	PPEB_LDR_DATA                 Ldr;
	PRTL_USER_PROCESS_PARAMETERS  ProcessParameters;
	PVOID                         Reserved4[3];
	PVOID                         AtlThunkSListPtr;
	PVOID                         Reserved5;
	ULONG                         Reserved6;
	PVOID                         Reserved7;
	ULONG                         Reserved8;
	ULONG                         AtlThunkSListPtr32;
	PVOID                         Reserved9[45];
	BYTE                          Reserved10[96];
	PPS_POST_PROCESS_INIT_ROUTINE PostProcessInitRoutine;
	BYTE                          Reserved11[128];
	PVOID                         Reserved12[1];
	ULONG                         SessionId;
} PEB, *PPEB;

// PROCESS_BASIC_INFORMATION for pure 32 and 64-bit processes
typedef struct _PROCESS_BASIC_INFORMATION {
	PVOID Reserved1;
	PVOID PebBaseAddress;
	PVOID Reserved2[2];
	ULONG_PTR UniqueProcessId;
	PVOID Reserved3;
} PROCESS_BASIC_INFORMATION;


WCHAR* QueryWorkingDirectory(DWORD pid)
{
	HANDLE handle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, pid);

	return QueryWorkingDirectory(handle);
}

WCHAR* QueryWorkingDirectory(HANDLE handle)
{
	// https://stackoverflow.com/questions/14018280/how-to-get-a-process-working-dir-on-windows
	PROCESS_BASIC_INFORMATION     pbi;
	RTL_USER_PROCESS_PARAMETERS upp;
	PEB   peb;
	ULONG len;

	_NtQueryInformationProcess NtQueryInformationProcess = (_NtQueryInformationProcess)GetProcAddress(GetModuleHandleA("ntdll.dll"), "NtQueryInformationProcess");
	NtQueryInformationProcess(handle, 0 /*ProcessBasicInformation*/, &pbi,
		sizeof(PROCESS_BASIC_INFORMATION), &len);

	SIZE_T l;
	ReadProcessMemory(handle, pbi.PebBaseAddress, &peb, sizeof(PEB), &l);
	ReadProcessMemory(handle, peb.ProcessParameters, &upp, sizeof(RTL_USER_PROCESS_PARAMETERS), &l);

	WCHAR* path = new WCHAR[upp.CurrentDirectoryPath.Length / 2 + 1];

	ReadProcessMemory(handle, upp.CurrentDirectoryPath.Buffer, path, upp.CurrentDirectoryPath.Length, &l);

	// null-terminate
	path[upp.CurrentDirectoryPath.Length / 2] = 0;

	return path;
}