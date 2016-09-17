// ****************************************************************************
// <copyright file="AssemblyInfo.cs" company="GalaSoft Laurent Bugnion">
// Copyright © GalaSoft Laurent Bugnion 2009-2016
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>3.11.2009</date>
// <project>GalaSoft.MvvmLight.Platform</project>
// <web>http://www.mvvmlight.net</web>
// <license>
// See license.txt in this project or http://www.galasoft.ch/license_MIT.txt
// </license>
// ****************************************************************************

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if !NETFX_CORE
#if !XAMARIN
using System.Windows.Markup;
#endif
#endif

[assembly: AssemblyTitle("GalaSoft.MvvmLight.Platform")]
[assembly: AssemblyDescription("Platform components to implement Model-View-ViewModel applications in WPF, Windows Store, Windows Phone, Silverlight and Xamarin. These are needed because of technical constraints with portable class libraries (PCL).")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("GalaSoft Laurent Bugnion @ http://www.galasoft.ch")]
[assembly: AssemblyProduct("GalaSoft.MvvmLight.Platform")]
[assembly: AssemblyCopyright("Copyright © GalaSoft Laurent Bugnion 2009-2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if !NETFX_CORE
#if !XAMARIN
#if !WINDOWS_PHONE
[assembly: XmlnsDefinition("http://www.galasoft.ch/mvvmlight", "GalaSoft.MvvmLight.Command")]
#endif
#endif
#endif

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly:NeutralResourcesLanguage("en-US")]

// BL0035
[assembly: AssemblyVersion("5.3.0.*")]
