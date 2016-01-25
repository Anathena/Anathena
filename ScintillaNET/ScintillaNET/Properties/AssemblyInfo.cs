using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScintillaNET")]
[assembly: AssemblyDescription("Source Editing Component")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jacob Slusser")]
[assembly: AssemblyProduct("ScintillaNET")]
[assembly: AssemblyCopyright("Copyright (c) 2016, Jacob Slusser. All rights reserved.")]
[assembly: AssemblyTrademark("The MIT License (MIT)")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f8ac48e7-9378-482d-8c7f-92c8408dd4f2")]

// http://stackoverflow.com/a/65062
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.5.6.0")]
[assembly: AssemblyFileVersion("3.5.6.0")]
[assembly: AssemblyInformationalVersion("3.5.6")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]

#if (DEBUG)
[assembly: InternalsVisibleToAttribute("ScintillaNET.Test")]
#endif
