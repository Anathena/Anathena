using Gecko;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Anathema.Source.Utils.Browser
{
    class BrowserHelper
    {
        // Singleton instance of Registration Manager
        private static Lazy<BrowserHelper> BrowserHelperInstance = new Lazy<BrowserHelper>(() => { return new BrowserHelper(); });

        private static Boolean RunOnce;
        private static Object InitializeLock;
        private List<String> BackgroundDownloadTags;
        private static String LastDownloadedFile;

        private BrowserHelper()
        {
            InitializeLock = new Object();
            BackgroundDownloadTags = new List<String>();
            RunOnce = true;
        }

        public static BrowserHelper GetInstance()
        {
            return BrowserHelperInstance.Value;
        }

        public void InitializeBrowserStatic(params String[] NewBackGroundDownloadTags)
        {
            using (TimedLock.Lock(InitializeLock))
            {
                if (NewBackGroundDownloadTags != null)
                    foreach (String Tag in NewBackGroundDownloadTags)
                        if (!BackgroundDownloadTags.Contains(Tag))
                            BackgroundDownloadTags.Add(Tag);

                if (!RunOnce)
                    return;

                if (!Environment.Is64BitProcess)
                    Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-32"));

                if (Environment.Is64BitProcess)
                    Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-64"));

                RunOnce = false;

                LauncherDialog.Download += LauncherDialog_Download;
            }
        }

        public String GetLastDownloadedFile()
        {
            using (TimedLock.Lock(InitializeLock))
            {
                return LastDownloadedFile;
            }
        }

        private Boolean URLMatchesTags(String Url)
        {
            foreach (String Tag in BackgroundDownloadTags)
                if (Url.Contains(Tag))
                    return true;

            return false;
        }

        #region Events

        /// <summary>
        ///  Handle save file dialog for downloads. Code ripped from the internet, no idea how it works.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void LauncherDialog_Download(Object Sender, LauncherDialogEvent E)
        {
            nsILocalFile ObjectTarget = Xpcom.CreateInstance<nsILocalFile>("@mozilla.org/file/local;1");
            Stream SaveStream;

            String FileName;
            String FileDirectory;

            using (nsAString nsAString = new nsAString(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp.tmp"))
            {
                ObjectTarget.InitWithPath(nsAString);
            }

            if (!URLMatchesTags(E.Url))
            {
                // Allow user to select the save location
                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
                SaveFileDialog.FilterIndex = 2;
                SaveFileDialog.RestoreDirectory = true;
                SaveFileDialog.FileName = E.Filename;

                if (SaveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                FileDirectory = Path.GetDirectoryName(SaveFileDialog.FileName);
                FileName = Path.GetFileName(SaveFileDialog.FileName);
            }
            else
            {
                // Tag matched; This URL will not allow the user to select a save location
                String TempFile = Path.GetTempFileName();

                FileDirectory = Path.GetDirectoryName(TempFile);
                FileName = Path.GetFileName(TempFile);
            }

            if ((SaveStream = File.OpenWrite(Path.Combine(FileDirectory, FileName))) == null)
                return;

            nsIURI Source = IOService.CreateNsIUri(E.Url);
            nsIURI Destination = IOService.CreateNsIUri(new Uri(Path.Combine(FileDirectory, FileName)).AbsoluteUri);
            nsAStringBase StringBase = new nsAString(Path.GetFileName(FileName));

            nsIWebBrowserPersist Persist = Xpcom.CreateInstance<nsIWebBrowserPersist>("@mozilla.org/embedding/browser/nsWebBrowserPersist;1");
            nsIDownloadManager DownloadMan = null;
            DownloadMan = Xpcom.CreateInstance<nsIDownloadManager>("@mozilla.org/download-manager;1");
            nsIDownload Download = DownloadMan.AddDownload(0, Source, Destination, StringBase, E.Mime, 0, null, Persist, false);

            if (Download != null)
            {
                Persist.SetPersistFlagsAttribute(2 | 32 | 16384);
                Persist.SetProgressListenerAttribute(Download);
                Persist.SaveURI(Source, null, null, null, null, (nsISupports)Destination, null);
            }
            SaveStream.Flush();
            SaveStream.Close();

            LastDownloadedFile = Path.Combine(FileDirectory, FileName);
        }

        #endregion

    } // End class

} // End namespace