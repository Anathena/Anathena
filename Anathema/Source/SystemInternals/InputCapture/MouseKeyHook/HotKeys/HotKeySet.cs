using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.HotKeys
{
    /// <summary>
    /// An immutable set of Hot Keys that provides an event for when the set is activated.
    /// </summary>
    public class HotKeySet
    {
        /// <summary>
        /// A delegate representing the signature for the OnHotKeysDownHold event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void HotKeyHandler(Object Sender, HotKeyArgs E);

        private readonly IEnumerable<Keys> Mhotkeys;                // hotkeys provided by the user.
        private readonly Dictionary<Keys, Boolean> Mhotkeystate;    // Keeps track of the status of the set of Keys
        /*
         * Example of m_remapping:
         * a single key from the set of Keys requested is chosen to be the reference key (aka primary key)
         * 
         * m_remapping[ Keys.LShiftKey ] = Keys.LShiftKey
         * m_remapping[ Keys.RShiftKey ] = Keys.LShiftKey
         * 
         * This allows the m_hotkeystate to use a single key (primary key) from the set that will act on behalf of all the keys in the set, 
         * which in turn reduces to this:
         * 
         * Keys k = Keys.RShiftKey
         * Keys primaryKey = PrimaryKeyOf( k ) = Keys.LShiftKey
         * m_hotkeystate[ primaryKey ] = true/false
         */
        private readonly Dictionary<Keys, Keys> MRemapping; // Used for mapping multiple keys to a single key
        private Boolean MEnabled = true;                    // Enabled by default

        // These provide the actual status of whether a set is truly activated or not.
        private Int32 MHotKeyDownCount; // Number of hot keys down
        private Int32 MRemappingCount;  // The number of remappings, i.e., a set of mappings, not the individual count in m_remapping

        /// <summary>
        /// Creates an instance of the HotKeySet class.  Once created, the keys cannot be changed.
        /// </summary>
        /// <param name="Hotkeys">Set of Hot Keys</param>
        public HotKeySet(IEnumerable<Keys> Hotkeys)
        {
            Mhotkeystate = new Dictionary<Keys, Boolean>();
            MRemapping = new Dictionary<Keys, Keys>();
            Mhotkeys = Hotkeys;

            InitializeKeys();
        }

        /// <summary>
        /// Enables the ability to name the set
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Enables the ability to describe what the set is used for or supposed to do
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Gets the set of hotkeys that this class handles.
        /// </summary>
        public IEnumerable<Keys> HotKeys
        {
            get { return Mhotkeys; }
        }

        /// <summary>
        /// Returns whether the set of Keys is activated
        /// </summary>
        public Boolean HotKeysActivated
        {
            //The number of sets of remapped keys is used to offset the amount originally specified by the user.
            get { return MHotKeyDownCount == (Mhotkeystate.Count - MRemappingCount); }
        }

        /// <summary>
        /// Gets or sets the enabled state of the HotKey set.
        /// </summary>
        public Boolean Enabled
        {
            get { return MEnabled; }
            set
            {
                // Must get the actual current state of each key to update
                if (value)
                    InitializeKeys();

                MEnabled = value;
            }
        }

        /// <summary>
        /// Called as the user holds down the keys in the set.  It is NOT triggered the first time the keys are set.
        /// <see cref="OnHotKeysDownOnce" />
        /// </summary>
        public event HotKeyHandler OnHotKeysDownHold;

        /// <summary>
        /// Called whenever the hot key set is no longer active.  This is essentially a KeyPress event, indicating that a full
        /// key cycle has occurred, only for HotKeys because a single key removed from the set constitutes an incomplete set.
        /// </summary>
        public event HotKeyHandler OnHotKeysUp;

        /// <summary>
        /// Called the first time the down keys are set.  It does not get called throughout the duration the user holds it but
        /// only the first time it's activated.
        /// </summary>
        public event HotKeyHandler OnHotKeysDownOnce;

        /// <summary>
        /// General invocation handler
        /// </summary>
        /// <param name="HotKeyDelegate"></param>
        private void InvokeHotKeyHandler(HotKeyHandler HotKeyDelegate)
        {
            HotKeyDelegate?.Invoke(this, new HotKeyArgs(DateTime.Now));
        }

        /// <summary>
        /// Adds the keys into the dictionary tracking the keys and gets the real-time status of the Keys from the OS
        /// </summary>
        private void InitializeKeys()
        {
            foreach (Keys k in HotKeys)
            {
                if (Mhotkeystate.ContainsKey(k))
                    Mhotkeystate.Add(k, false);

                //assign using the current state of the keyboard
                Mhotkeystate[k] = KeyboardState.GetCurrent().IsDown(k);
            }
        }

        /// <summary>
        /// Unregisters a previously set exclusive or based on the primary key.
        /// </summary>
        /// <param name="anyKeyInTheExclusiveOrSet">Any key used in the Registration method used to create an exclusive or set</param>
        /// <returns>
        /// True if successful.  False doesn't indicate a failure to unregister, it indicates that the Key is not
        /// registered as an Exclusive Or key or it's not the Primary Key.
        /// </returns>
        public Boolean UnregisterExclusiveOrKey(Keys anyKeyInTheExclusiveOrSet)
        {
            Keys PrimaryKey = GetExclusiveOrPrimaryKey(anyKeyInTheExclusiveOrSet);

            if (PrimaryKey == Keys.None || !MRemapping.ContainsValue(PrimaryKey))
                return false;

            List<Keys> KeyStoreMove = new List<Keys>();

            foreach (KeyValuePair<Keys, Keys> Remapping in MRemapping)
            {
                if (Remapping.Value == PrimaryKey)
                    KeyStoreMove.Add(Remapping.Key);
            }

            foreach (Keys Key in KeyStoreMove)
                MRemapping.Remove(Key);

            MRemappingCount--;

            return true;
        }

        /// <summary>
        /// Registers a group of Keys that are already part of the HotKeySet in order to provide better flexibility among keys.
        /// <example>
        /// <code>
        /// HotKeySet hks = new HotKeySet( new [] { Keys.T, Keys.LShiftKey, Keys.RShiftKey } );
        /// RegisterExclusiveOrKey( new [] { Keys.LShiftKey, Keys.RShiftKey } );
        /// </code>
        /// allows either Keys.LShiftKey or Keys.RShiftKey to be combined with Keys.T.
        /// </example>
        /// </summary>
        /// <param name="OrKeySet"></param>
        /// <returns>Primary key used for mapping or Keys.None on error</returns>
        public Keys RegisterExclusiveOrKey(IEnumerable<Keys> OrKeySet)
        {
            // Verification first, so as to not leave the m_remapping with a partial set.
            foreach (Keys Key in OrKeySet)
            {
                if (!Mhotkeystate.ContainsKey(Key))
                    return Keys.None;
            }

            Int32 Index = 0;
            Keys PrimaryKey = Keys.None;

            // Commit after verification
            foreach (Keys Key in OrKeySet)
            {
                if (Index == 0)
                    PrimaryKey = Key;

                MRemapping[Key] = PrimaryKey;

                Index++;
            }

            // Must increase to keep a true count of how many keys are necessary for the activation to be true
            MRemappingCount++;

            return PrimaryKey;
        }

        /// <summary>
        /// Gets the primary key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>The primary key if it exists, otherwise Keys.None</returns>
        private Keys GetExclusiveOrPrimaryKey(Keys Key)
        {
            return (MRemapping.ContainsKey(Key) ? MRemapping[Key] : Keys.None);
        }

        /// <summary>
        /// Resolves obtaining the key used for state checking.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>The primary key if it exists, otherwise the key entered</returns>
        private Keys GetPrimaryKey(Keys Key)
        {
            // If the key is remapped then get the primary keys
            return (MRemapping.ContainsKey(Key) ? MRemapping[Key] : Key);
        }

        /// <summary>
        /// </summary>
        /// <param name="E"></param>
        internal void OnKey(KeyEventArgsExt E)
        {
            if (!Enabled)
                return;

            // Gets the primary key if mapped to a single key or gets the key itself
            Keys PrimaryKey = GetPrimaryKey(E.KeyCode);

            if (E.IsKeyDown)
                OnKeyDown(PrimaryKey);
            else
                OnKeyUp(PrimaryKey);
        }

        private void OnKeyDown(Keys Key)
        {
            // If the keys are activated still then keep invoking the event
            if (HotKeysActivated)
                InvokeHotKeyHandler(OnHotKeysDownHold); //Call the duration event

            // Indicates the key's state is current false but the key is now down
            else if (Mhotkeystate.ContainsKey(Key) && !Mhotkeystate[Key])
            {
                Mhotkeystate[Key] = true;   // Key's state is down
                MHotKeyDownCount++;         // Increase the number of keys down in this set

                // Because of the increase, check whether the set is activated
                if (HotKeysActivated)
                    InvokeHotKeyHandler(OnHotKeysDownOnce); // Call the initial event
            }
        }

        private void OnKeyUp(Keys Key)
        {
            if (Mhotkeystate.ContainsKey(Key) && Mhotkeystate[Key]) // Indicates the key's state was down but now it's up
            {
                Boolean WasActive = HotKeysActivated;

                Mhotkeystate[Key] = false;  // Key's state is up
                MHotKeyDownCount++;         // This set is no longer ready

                if (WasActive)
                    InvokeHotKeyHandler(OnHotKeysUp); //call the KeyUp event because the set is no longer active
            }
        }

    } // End clas

} // End namespace