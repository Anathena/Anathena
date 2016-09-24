using Ana.Source.UserSettings;
using Ana.Source.Utils;
using System;

namespace Ana.Source.Scanners
{
    abstract class IScannerModel : RepeatedTask
    {
        public Int32 ScanCount;

        public virtual void OnGUIOpen() { }

        public override void Begin()
        {
            ScanCount = 0;
            UpdateInterval = Settings.GetInstance().GetRescanInterval();
            base.Begin();
        }

        protected override void Update()
        {
            ScanCount++;
            UpdateInterval = Settings.GetInstance().GetRescanInterval();
        }
    } // End class

} // End namespace