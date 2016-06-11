[Setup]
OutputBaseFileName=SlimDX SDK (March 2011)
AppName=SlimDX SDK Prerequisites (March 2011)
AppVerName=SlimDX SDK Prerequisites (March 2011)
AppPublisher=SlimDX Group
AppPublisherURL=http://slimdx.org
AppVersion=11.43
AppCopyright=Copyright (C) 2007-2011 SlimDX Group
VersionInfoProductName=SlimDX SDK (March 2011)
VersionInfoDescription=SlimDX SDK (March 2011)
VersionInfoVersion=11.43
Compression=lzma2
SolidCompression=yes
OutputDir=.
Uninstallable=no
CreateAppDir=no
;PrivilegesRequired=lowest
DefaultDirName={tmp}
DisableDirPage=yes
DisableFinishedPage=yes
DisableProgramGroupPage=yes
DisableReadyPage=yes
DisableReadyMemo=yes
DisableStartupPrompt=yes
DisableWelcomePage=yes
SetupIconFile=..\media\sdx_icon_black.ico
WizardSmallImageFile=ExtraFiles\logo-black-55.bmp

[Files]
Source: "ExtraFiles\VCRedist\vc10redist_x86.exe"; DestDir: {tmp}
Source: "ExtraFiles\VCRedist\vc10redist_x64.exe"; DestDir: {tmp}; Check: IsWin64
Source: "SlimDX SDK (March 2011).msi"; DestDir: "{tmp}"

[Run]
Filename: "{tmp}\vc10redist_x86.exe"; Parameters: "/passive"
Filename: "{tmp}\vc10redist_x64.exe"; Parameters: "/passive"; Check: IsWin64
Filename: "msiexec"; Parameters: "/i ""SlimDX SDK (March 2011).msi"" "; WorkingDir: "{tmp}"

[Code]
//http://www.kynosarges.de/DotNetVersion.html
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;
    // .NET 4.0 uses value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;
    result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;
begin
    if not IsDotNetDetected('v2.0.50727', 0)
    and not IsDotNetDetected('v3.5', 0)
    and not IsDotNetDetected('v4\Client', 0)
    and not IsDotNetDetected('v4\Full', 0)
    then begin
        MsgBox('SlimDX requires Microsoft .NET Framework 2.0 or later.'#13#13
            'Please use Windows Update to install it,'#13
            'and then re-run the setup program.', mbInformation, MB_OK);
        result := false;
    end else
        result := true;
end;
