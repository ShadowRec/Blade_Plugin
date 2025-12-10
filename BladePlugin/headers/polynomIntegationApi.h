#pragma once


typedef long (WINAPI *FuncPLConnect)(LPCWSTR connectionString, BOOL isWindowsAuthorization, LPCWSTR userName, LPCWSTR password);
typedef long (WINAPI *FuncPLDisconnect)();
typedef long (WINAPI *FuncLibInterfaceNotifyEntry)(IDispatch kompasObj);
typedef void (WINAPI *FuncPLInsert3D)(LPDISPATCH* retVal, IBOObject* object, kAPI5::ksPlacement* aPlacement);
typedef void (WINAPI *FuncPLInsertByLocation3D)(LPDISPATCH* retVal, LPCWSTR location, kAPI5::ksPlacement* aPlacement);

