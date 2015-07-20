using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class InputCorrelator
    {
        // Input correlation related
        private readonly IKeyboardMouseEvents InputHook;            // Input Capturing instance
        private Dictionary<Keys, Double> PhiCoefficientCorrelation; // Correlation of input and memory changes
        private Dictionary<Keys, List<DateTime>> KeyBoardDown;      // List of keyboard down events
        private Dictionary<Keys, List<DateTime>> KeyBoardUp;        // List of keyboard up events

        public InputCorrelator()
        {
            KeyBoardUp = new Dictionary<Keys, List<DateTime>>();
            KeyBoardDown = new Dictionary<Keys, List<DateTime>>();

            // TODO: App hook option? From author: "Note: for the application hook, use the Hook.AppEvents() instead"
            InputHook = Hook.GlobalEvents();
        }

        public void Begin()
        {
            // Create input hook events
            InputHook.MouseDownExt += GlobalHookMouseDownExt;
            InputHook.KeyUp += GlobalHookKeyUp;
            InputHook.KeyDown += GlobalHookKeyDown;
        }

        public void End()
        {
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyDown -= GlobalHookKeyDown;

            //It is recommened to dispose it
            InputHook.Dispose();
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            // Releasing a key that we have no record of ever pressing -- just ignore it
            if (!KeyBoardDown.ContainsKey(e.KeyCode))
                return;

            if (!KeyBoardUp.ContainsKey(e.KeyCode))
                KeyBoardUp.Add(e.KeyCode, new List<DateTime>());

            KeyBoardUp[e.KeyCode].Add(DateTime.Now);

            EnforceSameInputListSizes(e.KeyCode);
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            // Console.WriteLine("KeyPress: \t{0} \t{1}", e.KeyChar, DateTime.Now);
            if (!KeyBoardDown.ContainsKey(e.KeyCode))
                KeyBoardDown.Add(e.KeyCode, new List<DateTime>());

            KeyBoardDown[e.KeyCode].Add(DateTime.Now);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        private void EnforceSameInputListSizes(Keys KeyCode, bool EnforceTime = false)
        {
            // Ensure that the sizes of up and down key records match (should not happen often, but it should be handled)
            if (KeyBoardUp[KeyCode].Count > KeyBoardDown[KeyCode].Count)
                KeyBoardUp[KeyCode].RemoveRange(KeyBoardDown[KeyCode].Count, KeyBoardUp[KeyCode].Count - KeyBoardDown[KeyCode].Count);

            if (KeyBoardDown[KeyCode].Count > KeyBoardUp[KeyCode].Count)
                KeyBoardDown[KeyCode].RemoveRange(KeyBoardUp[KeyCode].Count, KeyBoardDown[KeyCode].Count - KeyBoardUp[KeyCode].Count);

            if (!EnforceTime)
                return;

            // TODO: Ensure that the release time isnt > start time, etc
        }
        
        private void MeasurePhiCoefficients()
        {
            // Ensure that the down and up lists are in sync
            foreach (KeyValuePair<Keys, List<DateTime>> NextItem in KeyBoardUp)
                EnforceSameInputListSizes(NextItem.Key, true);

            // Create a single list which combines the list of key presses (down) and releases (up)
            var KeyBoardDurations = KeyBoardDown.Zip(KeyBoardUp, (D, U) => new { Down = D, Up = U });

            foreach (var NextItem in KeyBoardDurations)
            {
                List<DateTime> KeyDownHistory = NextItem.Down.Value;
                List<DateTime> KeyUpHistory = NextItem.Up.Value;
                /*
                // Test every recorded memory change (or lack thereof) against the input change logs
                for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
                {
                    // https://en.wikipedia.org/wiki/Point-biserial_correlation_coefficient

                    Single Count0 = 0;
                    Single Count1 = 0;
                    Single M0 = 0;
                    Single M1 = 0;
                    Single Sn = 0;

                    Single ActivationRatio = 0;
                    Single Duration = 0;
                    Single InputActivatedDuration = 0;

                    for (int ChangeIndex = 0; ChangeIndex < MemoryPages[PageIndex].ChangeHistory.Count; ChangeIndex++)
                    {
                        bool PageChanged = MemoryPages[PageIndex].ChangeHistory[ChangeIndex];
                        bool InputActive = false;

                        for (int InputIndex = 0; InputIndex < KeyDownHistory.Count; InputIndex++)
                        {
                            // Search for the most temporally close page index that matches the input change
                            if (!(MemoryPages[PageIndex].DateHistory[ChangeIndex] > KeyDownHistory[InputIndex] &&
                                MemoryPages[PageIndex].DateHistory[ChangeIndex] < KeyUpHistory[InputIndex].AddMilliseconds(DelayBuffer)))
                                continue;

                            // We found a match indicating the input was active at the same time (or at least within DelayBuffer ms)
                            InputActive = true;

                            // Add to the total time activated
                            InputActivatedDuration += (Single)(KeyUpHistory[InputIndex].Ticks - KeyDownHistory[InputIndex].Ticks);

                            // Compute the difference between the start and end to get the total time
                            if (InputIndex == KeyDownHistory.Count - 1)
                                Duration = (Single)(KeyUpHistory[InputIndex].Ticks - KeyDownHistory[0].Ticks);
                        }

                        if (InputActive)
                            M1++;
                        else
                            M0++;


                        if (PageChanged || InputActive)
                            Count1++;
                        else
                            Count0++;
                    }

                    M0 /= Count0;
                    M1 /= Count1;

                    ActivationRatio = InputActivatedDuration / Duration;
                    Sn = (Single)Math.Sqrt(ActivationRatio * (1.0f - ActivationRatio));

                    MemoryPages[PageIndex].PhiCoefficient = (M1 - M0) / Sn *
                        (Single)Math.Sqrt(Count0 * Count1 / (Count0 + Count1) * (Count0 + Count1));

                    //MemoryPages[PageIndex].PhiCoefficient = (Count11 * Count00 - Count10 * Count01) /
                    //    (Single)Math.Sqrt(Count0 * Count0 * Count1 * Count1);
                }*/
            }
        }
    }
}
