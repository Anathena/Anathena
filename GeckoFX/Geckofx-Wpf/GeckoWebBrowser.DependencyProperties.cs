using System.Windows;

namespace Gecko
{
    partial class GeckoWebBrowser
    {
        private static readonly DependencyPropertyKey StatusPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Status",
                typeof(string),
                typeof(GeckoWebBrowser),
                new PropertyMetadata(null));

        public static readonly DependencyProperty StatusProperty =
            StatusPropertyKey.DependencyProperty;

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            private set { SetValue(StatusPropertyKey, value); }
        }


        private static readonly DependencyPropertyKey DocumentTitlePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "DocumentTitle",
                typeof(string),
                typeof(GeckoWebBrowser),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DocumentTitleProperty =
            StatusPropertyKey.DependencyProperty;

        public string DocumentTitle
        {
            get { return (string)GetValue(DocumentTitleProperty); }
            private set { SetValue(DocumentTitlePropertyKey, value); }
        }
    }
}
