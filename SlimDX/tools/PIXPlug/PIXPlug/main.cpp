#include <metahost.h>
#include <windows.h>
#include <string>
#include <atlbase.h>

#import "mscorlib.tlb" raw_interfaces_only high_property_prefixes("_get","_put","_putref") rename("ReportEvent", "InteropServices_ReportEvent")
using namespace mscorlib;

#pragma comment(lib, "mscoree.lib")

#define FAIL_IF(condition, message, result) if (condition) {  MessageBox(0, (message), L"PIXPlug", MB_OK); return result; }
#define FAIL_HR(hr, message, result) FAIL_IF(FAILED(hr), (message), (result))

template<typename from, typename to>
from lexical_cast(const to &arg);

template<>
inline std::wstring lexical_cast<std::wstring, std::string>(const std::string &arg)
{
	std::wstring result;
	std::locale loc;

	for (size_t i = 0; i < arg.size(); ++i)
		result += std::use_facet<std::ctype<wchar_t>>(loc).widen(arg[i]);

	return result;
}

int main(int argc, char **argv)
{
	FAIL_IF(argc != 2, L"Invalid number of parameters passed to PIXPlug. Usage:\nPIXPlug.exe path-to-assembly", -1)

	CComPtr<ICLRMetaHostPolicy> metaHostPolicy;
	HRESULT hr = CLRCreateInstance(CLSID_CLRMetaHostPolicy, IID_PPV_ARGS(&metaHostPolicy));
	FAIL_HR(hr, L"Failed to initialize the CLR hosting API.", -2);

	std::wstring assembly = lexical_cast<std::wstring, std::string>(argv[1]);

	DWORD cchVersion = 0;
	CComPtr<ICLRRuntimeInfo> runtimeInfo;
	hr = metaHostPolicy->GetRequestedRuntime(
		static_cast<METAHOST_POLICY_FLAGS>(METAHOST_POLICY_HIGHCOMPAT | METAHOST_POLICY_APPLY_UPGRADE_POLICY),
		assembly.c_str(),
		0, 0,
		&cchVersion,
		0, 0, 0,
		IID_PPV_ARGS(&runtimeInfo)
	);
	FAIL_HR(hr, L"Failed to load the specified assembly.", -3);

	CComPtr<ICorRuntimeHost> runtime;
	hr = runtimeInfo->GetInterface(CLSID_CorRuntimeHost, IID_PPV_ARGS(&runtime));
	FAIL_HR(hr, L"Failed to get the CLR runtime host interface.", -4);

	hr = runtime->Start();
	FAIL_HR(hr, L"Failed to start the CLR.", -5);

	IUnknown *appDomainThunk;
	hr = runtime->GetDefaultDomain(&appDomainThunk);
	FAIL_HR(hr, L"Failed to retrieve the default AppDomain.", -6);

	CComPtr<_AppDomain> appDomain;
	hr = appDomainThunk->QueryInterface(IID_PPV_ARGS(&appDomain));
	FAIL_HR(hr, L"Failed to retrieve the default AppDomain.", -7);

	long result = 0;
	bstr_t bstrAssembly(assembly.c_str());
	hr = appDomain->ExecuteAssembly_2(bstrAssembly, &result);
	FAIL_HR(hr, L"Failed to execute the provided assembly.", -8);

	runtime->Stop();

	return result;
}