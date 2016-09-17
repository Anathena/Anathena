namespace Ana.Source.Mvvm.Messaging
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Threading;

    /// <summary>
    /// The Messenger is a class allowing objects to exchange messages.
    /// </summary>
    public class Messenger : IMessenger
    {
        private static readonly object CreationLock = new object();
        private static IMessenger _defaultInstance;
        private readonly object _registerLock = new object();
        private Dictionary<Type, List<WeakActionAndToken>> _recipientsOfSubclassesAction;
        private Dictionary<Type, List<WeakActionAndToken>> _recipientsStrictAction;

        /// <summary>
        /// Gets the Messenger's default instance, allowing
        /// to register and send messages in a static manner.
        /// </summary>
        public static IMessenger Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        if (_defaultInstance == null)
                        {
                            _defaultInstance = new Messenger();
                        }
                    }
                }

                return _defaultInstance;
            }
        }

        #region IMessenger Members

        /// <summary>
        /// Registers a recipient for a type of message TMessage. The action
        /// parameter will be executed when a corresponding message is sent.
        /// <para>Registering a recipient does not create a hard reference to it,
        /// so if this recipient is deleted, no memory leak is caused.</para>
        /// </summary>
        /// <typeparam name="TMessage">The type of message that the recipient registers
        /// for.</typeparam>
        /// <param name="recipient">The recipient that will receive the messages.</param>
        /// <param name="action">The action that will be executed when a message
        /// of type TMessage is sent. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        public virtual void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Register(recipient, null, false, action);
        }

        /// <summary>
        /// Registers a recipient for a type of message TMessage.
        /// The action parameter will be executed when a corresponding 
        /// message is sent. See the receiveDerivedMessagesToo parameter
        /// for details on how messages deriving from TMessage (or, if TMessage is an interface,
        /// messages implementing TMessage) can be received too.
        /// <para>Registering a recipient does not create a hard reference to it,
        /// so if this recipient is deleted, no memory leak is caused.</para>
        /// </summary>
        /// <typeparam name="TMessage">The type of message that the recipient registers
        /// for.</typeparam>
        /// <param name="recipient">The recipient that will receive the messages.</param>
        /// <param name="receiveDerivedMessagesToo">If true, message types deriving from
        /// TMessage will also be transmitted to the recipient. For example, if a SendOrderMessage
        /// and an ExecuteOrderMessage derive from OrderMessage, registering for OrderMessage
        /// and setting receiveDerivedMessagesToo to true will send SendOrderMessage
        /// and ExecuteOrderMessage to the recipient that registered.
        /// <para>Also, if TMessage is an interface, message types implementing TMessage will also be
        /// transmitted to the recipient. For example, if a SendOrderMessage
        /// and an ExecuteOrderMessage implement IOrderMessage, registering for IOrderMessage
        /// and setting receiveDerivedMessagesToo to true will send SendOrderMessage
        /// and ExecuteOrderMessage to the recipient that registered.</para>
        /// </param>
        /// <param name="action">The action that will be executed when a message
        /// of type TMessage is sent. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        public virtual void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action)
        {
            Register(recipient, null, receiveDerivedMessagesToo, action);
        }

        /// <summary>
        /// Registers a recipient for a type of message TMessage.
        /// The action parameter will be executed when a corresponding 
        /// message is sent.
        /// <para>Registering a recipient does not create a hard reference to it,
        /// so if this recipient is deleted, no memory leak is caused.</para>
        /// </summary>
        /// <typeparam name="TMessage">The type of message that the recipient registers
        /// for.</typeparam>
        /// <param name="recipient">The recipient that will receive the messages.</param>
        /// <param name="token">A token for a messaging channel. If a recipient registers
        /// using a token, and a sender sends a message using the same token, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a token when registering (or who used a different token) will not
        /// get the message. Similarly, messages sent without any token, or with a different
        /// token, will not be delivered to that recipient.</param>
        /// <param name="action">The action that will be executed when a message
        /// of type TMessage is sent. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        public virtual void Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            Register(recipient, token, false, action);
        }

        /// <summary>
        /// Registers a recipient for a type of message TMessage.
        /// The action parameter will be executed when a corresponding 
        /// message is sent. See the receiveDerivedMessagesToo parameter
        /// for details on how messages deriving from TMessage (or, if TMessage is an interface,
        /// messages implementing TMessage) can be received too.
        /// <para>Registering a recipient does not create a hard reference to it,
        /// so if this recipient is deleted, no memory leak is caused.</para>
        /// </summary>
        /// <typeparam name="TMessage">The type of message that the recipient registers
        /// for.</typeparam>
        /// <param name="recipient">The recipient that will receive the messages.</param>
        /// <param name="token">A token for a messaging channel. If a recipient registers
        /// using a token, and a sender sends a message using the same token, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a token when registering (or who used a different token) will not
        /// get the message. Similarly, messages sent without any token, or with a different
        /// token, will not be delivered to that recipient.</param>
        /// <param name="receiveDerivedMessagesToo">If true, message types deriving from
        /// TMessage will also be transmitted to the recipient. For example, if a SendOrderMessage
        /// and an ExecuteOrderMessage derive from OrderMessage, registering for OrderMessage
        /// and setting receiveDerivedMessagesToo to true will send SendOrderMessage
        /// and ExecuteOrderMessage to the recipient that registered.
        /// <para>Also, if TMessage is an interface, message types implementing TMessage will also be
        /// transmitted to the recipient. For example, if a SendOrderMessage
        /// and an ExecuteOrderMessage implement IOrderMessage, registering for IOrderMessage
        /// and setting receiveDerivedMessagesToo to true will send SendOrderMessage
        /// and ExecuteOrderMessage to the recipient that registered.</para>
        /// </param>
        /// <param name="action">The action that will be executed when a message
        /// of type TMessage is sent. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        public virtual void Register<TMessage>(
            object recipient,
            object token,
            bool receiveDerivedMessagesToo,
            Action<TMessage> action)
        {
            lock (_registerLock)
            {
                var messageType = typeof(TMessage);

                Dictionary<Type, List<WeakActionAndToken>> recipients;

                if (receiveDerivedMessagesToo)
                {
                    if (_recipientsOfSubclassesAction == null)
                    {
                        _recipientsOfSubclassesAction = new Dictionary<Type, List<WeakActionAndToken>>();
                    }

                    recipients = _recipientsOfSubclassesAction;
                }
                else
                {
                    if (_recipientsStrictAction == null)
                    {
                        _recipientsStrictAction = new Dictionary<Type, List<WeakActionAndToken>>();
                    }

                    recipients = _recipientsStrictAction;
                }

                lock (recipients)
                {
                    List<WeakActionAndToken> list;

                    if (!recipients.ContainsKey(messageType))
                    {
                        list = new List<WeakActionAndToken>();
                        recipients.Add(messageType, list);
                    }
                    else
                    {
                        list = recipients[messageType];
                    }

                    var weakAction = new WeakAction<TMessage>(recipient, action);

                    var item = new WeakActionAndToken
                    {
                        Action = weakAction,
                        Token = token
                    };

                    list.Add(item);
                }
            }

            RequestCleanup();
        }

        private bool _isCleanupRegistered;

        /// <summary>
        /// Sends a message to registered recipients. The message will
        /// reach all recipients that registered for this message type
        /// using one of the Register methods.
        /// </summary>
        /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
        /// <param name="message">The message to send to registered recipients.</param>
        public virtual void Send<TMessage>(TMessage message)
        {
            SendToTargetOrType(message, null, null);
        }

        /// <summary>
        /// Sends a message to registered recipients. The message will
        /// reach only recipients that registered for this message type
        /// using one of the Register methods, and that are
        /// of the targetType.
        /// </summary>
        /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
        /// <typeparam name="TTarget">The type of recipients that will receive
        /// the message. The message won't be sent to recipients of another type.</typeparam>
        /// <param name="message">The message to send to registered recipients.</param>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Send<TMessage, TTarget>(TMessage message)
        {
            SendToTargetOrType(message, typeof(TTarget), null);
        }

        /// <summary>
        /// Sends a message to registered recipients. The message will
        /// reach only recipients that registered for this message type
        /// using one of the Register methods, and that are
        /// of the targetType.
        /// </summary>
        /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
        /// <param name="message">The message to send to registered recipients.</param>
        /// <param name="token">A token for a messaging channel. If a recipient registers
        /// using a token, and a sender sends a message using the same token, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a token when registering (or who used a different token) will not
        /// get the message. Similarly, messages sent without any token, or with a different
        /// token, will not be delivered to that recipient.</param>
        public virtual void Send<TMessage>(TMessage message, object token)
        {
            SendToTargetOrType(message, null, token);
        }

        /// <summary>
        /// Unregisters a messager recipient completely. After this method
        /// is executed, the recipient will not receive any messages anymore.
        /// </summary>
        /// <param name="recipient">The recipient that must be unregistered.</param>
        public virtual void Unregister(object recipient)
        {
            UnregisterFromLists(recipient, _recipientsOfSubclassesAction);
            UnregisterFromLists(recipient, _recipientsStrictAction);
        }

        /// <summary>
        /// Unregisters a message recipient for a given type of messages only. 
        /// After this method is executed, the recipient will not receive messages
        /// of type TMessage anymore, but will still receive other message types (if it
        /// registered for them previously).
        /// </summary>
        /// <param name="recipient">The recipient that must be unregistered.</param>
        /// <typeparam name="TMessage">The type of messages that the recipient wants
        /// to unregister from.</typeparam>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Unregister<TMessage>(object recipient)
        {
            Unregister<TMessage>(recipient, null, null);
        }

        /// <summary>
        /// Unregisters a message recipient for a given type of messages only and for a given token. 
        /// After this method is executed, the recipient will not receive messages
        /// of type TMessage anymore with the given token, but will still receive other message types
        /// or messages with other tokens (if it registered for them previously).
        /// </summary>
        /// <param name="recipient">The recipient that must be unregistered.</param>
        /// <param name="token">The token for which the recipient must be unregistered.</param>
        /// <typeparam name="TMessage">The type of messages that the recipient wants
        /// to unregister from.</typeparam>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Unregister<TMessage>(object recipient, object token)
        {
            Unregister<TMessage>(recipient, token, null);
        }

        /// <summary>
        /// Unregisters a message recipient for a given type of messages and for
        /// a given action. Other message types will still be transmitted to the
        /// recipient (if it registered for them previously). Other actions that have
        /// been registered for the message type TMessage and for the given recipient (if
        /// available) will also remain available.
        /// </summary>
        /// <typeparam name="TMessage">The type of messages that the recipient wants
        /// to unregister from.</typeparam>
        /// <param name="recipient">The recipient that must be unregistered.</param>
        /// <param name="action">The action that must be unregistered for
        /// the recipient and for the message type TMessage.</param>
        public virtual void Unregister<TMessage>(object recipient, Action<TMessage> action)
        {
            Unregister(recipient, null, action);
        }

        /// <summary>
        /// Unregisters a message recipient for a given type of messages, for
        /// a given action and a given token. Other message types will still be transmitted to the
        /// recipient (if it registered for them previously). Other actions that have
        /// been registered for the message type TMessage, for the given recipient and other tokens (if
        /// available) will also remain available.
        /// </summary>
        /// <typeparam name="TMessage">The type of messages that the recipient wants
        /// to unregister from.</typeparam>
        /// <param name="recipient">The recipient that must be unregistered.</param>
        /// <param name="token">The token for which the recipient must be unregistered.</param>
        /// <param name="action">The action that must be unregistered for
        /// the recipient and for the message type TMessage.</param>
        public virtual void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            UnregisterFromLists(recipient, token, action, _recipientsStrictAction);
            UnregisterFromLists(recipient, token, action, _recipientsOfSubclassesAction);
            RequestCleanup();
        }

        #endregion

        /// <summary>
        /// Provides a way to override the Messenger.Default instance with
        /// a custom instance, for example for unit testing purposes.
        /// </summary>
        /// <param name="newMessenger">The instance that will be used as Messenger.Default.</param>
        public static void OverrideDefault(IMessenger newMessenger)
        {
            _defaultInstance = newMessenger;
        }

        /// <summary>
        /// Sets the Messenger's default (static) instance to null.
        /// </summary>
        public static void Reset()
        {
            _defaultInstance = null;
        }

        /// <summary>
        /// Provides a non-static access to the static <see cref="Reset"/> method.
        /// Sets the Messenger's default (static) instance to null.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Non static access is needed.")]
        public void ResetAll()
        {
            Reset();
        }

        private static void CleanupList(IDictionary<Type, List<WeakActionAndToken>> lists)
        {
            if (lists == null)
            {
                return;
            }

            lock (lists)
            {
                var listsToRemove = new List<Type>();
                foreach (var list in lists)
                {
                    var recipientsToRemove = list.Value
                        .Where(item => item.Action == null || !item.Action.IsAlive)
                        .ToList();

                    foreach (var recipient in recipientsToRemove)
                    {
                        list.Value.Remove(recipient);
                    }

                    if (list.Value.Count == 0)
                    {
                        listsToRemove.Add(list.Key);
                    }
                }

                foreach (var key in listsToRemove)
                {
                    lists.Remove(key);
                }
            }
        }

        private static void SendToList<TMessage>(TMessage message, IEnumerable<WeakActionAndToken> weakActionsAndTokens, Type messageTargetType, object token)
        {
            if (weakActionsAndTokens != null)
            {
                // Clone to protect from people registering in a "receive message" method
                // Correction Messaging BL0004.007
                var list = weakActionsAndTokens.ToList();
                var listClone = list.Take(list.Count()).ToList();

                foreach (var item in listClone)
                {
                    var executeAction = item.Action as IExecuteWithObject;

                    if (executeAction != null
                        && item.Action.IsAlive
                        && item.Action.Target != null
                        && (messageTargetType == null
                            || item.Action.Target.GetType() == messageTargetType
                            || messageTargetType.IsAssignableFrom(item.Action.Target.GetType()))
                        && ((item.Token == null && token == null)
                            || item.Token != null && item.Token.Equals(token)))
                    {
                        executeAction.ExecuteWithObject(message);
                    }
                }
            }
        }

        private static void UnregisterFromLists(object recipient, Dictionary<Type, List<WeakActionAndToken>> lists)
        {
            if (recipient == null || lists == null || lists.Count == 0)
            {
                return;
            }

            lock (lists)
            {
                foreach (var messageType in lists.Keys)
                {
                    foreach (var item in lists[messageType])
                    {
                        var weakAction = (IExecuteWithObject)item.Action;

                        if (weakAction != null
                            && recipient == weakAction.Target)
                        {
                            weakAction.MarkForDeletion();
                        }
                    }
                }
            }
        }

        private static void UnregisterFromLists<TMessage>(object recipient, object token, Action<TMessage> action, Dictionary<Type, List<WeakActionAndToken>> lists)
        {
            var messageType = typeof(TMessage);

            if (recipient == null
                || lists == null
                || lists.Count == 0
                || !lists.ContainsKey(messageType))
            {
                return;
            }

            lock (lists)
            {
                foreach (var item in lists[messageType])
                {
                    var weakActionCasted = item.Action as WeakAction<TMessage>;

                    if (weakActionCasted != null
                        && recipient == weakActionCasted.Target
                        && (action == null
                            || action.Method.Name == weakActionCasted.MethodName)
                        && (token == null
                            || token.Equals(item.Token)))
                    {
                        item.Action.MarkForDeletion();
                    }
                }
            }
        }

        /// <summary>
        /// Notifies the Messenger that the lists of recipients should
        /// be scanned and cleaned up.
        /// Since recipients are stored as <see cref="WeakReference"/>, 
        /// recipients can be garbage collected even though the Messenger keeps 
        /// them in a list. During the cleanup operation, all "dead"
        /// recipients are removed from the lists. Since this operation
        /// can take a moment, it is only executed when the application is
        /// idle. For this reason, a user of the Messenger class should use
        /// <see cref="RequestCleanup"/> instead of forcing one with the 
        /// <see cref="Cleanup" /> method.
        /// </summary>
        public void RequestCleanup()
        {
            if (!_isCleanupRegistered)
            {
                Action cleanupAction = Cleanup;

                Dispatcher.CurrentDispatcher.BeginInvoke(
                    cleanupAction,
                    DispatcherPriority.ApplicationIdle,
                    null);
                _isCleanupRegistered = true;
            }
        }

        /// <summary>
        /// Scans the recipients' lists for "dead" instances and removes them.
        /// Since recipients are stored as <see cref="WeakReference"/>, 
        /// recipients can be garbage collected even though the Messenger keeps 
        /// them in a list. During the cleanup operation, all "dead"
        /// recipients are removed from the lists. Since this operation
        /// can take a moment, it is only executed when the application is
        /// idle. For this reason, a user of the Messenger class should use
        /// <see cref="RequestCleanup"/> instead of forcing one with the 
        /// <see cref="Cleanup" /> method.
        /// </summary>
        public void Cleanup()
        {
            CleanupList(_recipientsOfSubclassesAction);
            CleanupList(_recipientsStrictAction);
            _isCleanupRegistered = false;
        }

        private void SendToTargetOrType<TMessage>(TMessage message, Type messageTargetType, object token)
        {
            var messageType = typeof(TMessage);

            if (_recipientsOfSubclassesAction != null)
            {
                // Clone to protect from people registering in a "receive message" method
                // Correction Messaging BL0008.002
                var listClone =
                    _recipientsOfSubclassesAction.Keys.Take(_recipientsOfSubclassesAction.Count()).ToList();

                foreach (var type in listClone)
                {
                    List<WeakActionAndToken> list = null;

                    if (messageType == type || messageType.IsSubclassOf(type) || type.IsAssignableFrom(messageType))
                    {
                        lock (_recipientsOfSubclassesAction)
                        {
                            list = _recipientsOfSubclassesAction[type].Take(_recipientsOfSubclassesAction[type].Count()).ToList();
                        }
                    }

                    SendToList(message, list, messageTargetType, token);
                }
            }

            if (_recipientsStrictAction != null)
            {
                List<WeakActionAndToken> list = null;

                lock (_recipientsStrictAction)
                {
                    if (_recipientsStrictAction.ContainsKey(messageType))
                    {
                        list = _recipientsStrictAction[messageType]
                            .Take(_recipientsStrictAction[messageType].Count())
                            .ToList();
                    }
                }

                if (list != null)
                {
                    SendToList(message, list, messageTargetType, token);
                }
            }

            RequestCleanup();
        }

        #region Nested type: WeakActionAndToken

        private struct WeakActionAndToken
        {
            public WeakAction Action;

            public object Token;
        }

        #endregion
    }
    //// End class
}
//// End namespace