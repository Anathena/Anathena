namespace GalaSoft.MvvmLight.Helpers
{
    using System;

    /// <summary>
    /// Helper class for platform detection.
    /// </summary>
    internal static class DesignerLibrary
    {
        internal static DesignerPlatformLibrary DetectedDesignerLibrary
        {
            get
            {
                if (_detectedDesignerPlatformLibrary == null)
                {
                    _detectedDesignerPlatformLibrary = GetCurrentPlatform();
                }
                return _detectedDesignerPlatformLibrary.Value;
            }
        }

        private static DesignerPlatformLibrary GetCurrentPlatform()
        {
            // We check Silverlight first because when in the VS designer, the .NET libraries will resolve
            // If we can resolve the SL libs, then we're in SL or WP
            // Then we check .NET because .NET will load the WinRT library (even though it can't really run it)
            // When running in WinRT, it will not load the PresentationFramework lib

            // Check Silverlight
            var dm = Type.GetType("System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
            if (dm != null)
            {
                return DesignerPlatformLibrary.Silverlight;
            }

            // Check .NET 
            var cmdm = Type.GetType("System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            if (cmdm != null) // loaded the assembly, could be .net 
            {
                return DesignerPlatformLibrary.Net;
            }

            // check WinRT next
            var wadm = Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime");
            if (wadm != null)
            {
                return DesignerPlatformLibrary.WinRt;
            }

            return DesignerPlatformLibrary.Unknown;
        }


        private static DesignerPlatformLibrary? _detectedDesignerPlatformLibrary;
    }

    internal enum DesignerPlatformLibrary
    {
        Unknown,
        Net,
        WinRt,
        Silverlight
    }
    //// End enum
}
//// End namespace