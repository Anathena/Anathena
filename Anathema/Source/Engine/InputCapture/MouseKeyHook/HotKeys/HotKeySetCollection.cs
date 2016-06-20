using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.HotKeys
{
    /// <summary>
    ///     A collection of HotKeySets
    /// </summary>
    public sealed class HotKeySetCollection : List<HotKeySet>
    {
        private KeyChainHandler MKeyChain;

        /// <summary>
        ///     Adds a HotKeySet to the collection.
        /// </summary>
        /// <param name="HotKeySet"></param>
        public new void Add(HotKeySet HotKeySet)
        {
            MKeyChain += HotKeySet.OnKey;
            base.Add(HotKeySet);
        }

        /// <summary>
        ///     Removes the HotKeySet from the collection.
        /// </summary>
        /// <param name="HotKeySet"></param>
        public new void Remove(HotKeySet HotKeySet)
        {
            MKeyChain -= HotKeySet.OnKey;
            base.Remove(HotKeySet);
        }

        /// <summary>
        ///     Uses a multi-case delegate to invoke individual HotKeySets if the Key is in use by any HotKeySets.
        /// </summary>
        /// <param name="E"></param>
        internal void OnKey(KeyEventArgsExt E)
        {
            MKeyChain?.Invoke(E);
        }

        private delegate void KeyChainHandler(KeyEventArgsExt Args);

    } // End class

} // End namespace