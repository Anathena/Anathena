namespace Ana.Source.Mvvm.Helpers
{
    using System.Reflection;

    /// <summary>
    /// Helper class for platform and feature detection.
    /// </summary>
    static class FeatureDetection
    {
        private static bool? _isPrivateReflectionSupported;

        public static bool IsPrivateReflectionSupported
        {
            get
            {
                if (_isPrivateReflectionSupported == null)
                    _isPrivateReflectionSupported = ResolveIsPrivateReflectionSupported();

                return _isPrivateReflectionSupported.Value;
            }
        }

        private static bool ResolveIsPrivateReflectionSupported()
        {
            var inst = new ReflectionDetectionClass();

            try
            {
                var method = typeof(ReflectionDetectionClass).GetTypeInfo().GetDeclaredMethod("Method");
                method.Invoke(inst, null);
            }
            catch // If we get here, then our platform doesn't support private reflection
            {
                return false;
            }

            return true;
        }

        private class ReflectionDetectionClass
        {
            private void Method()
            {

            }
        }
    }
    //// End class
}
//// End namespace