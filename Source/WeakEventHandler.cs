using System;
using System.Collections.Specialized;
using System.Reflection;

namespace MyApp.Core
{
    /// This approach to weak event handlers can be found in various online posts for solving the problem.
    ///     Paul Stovell: http://paulstovell.com/blog/weakevents
    ///     Dustin Campbell: http://diditwith.net/2007/03/23/SolvingTheProblemWithEventsWeakEventHandlers.aspx
    /// ...and more. Not sure which one was the original inspiration but additions have been made here.

    /// <summary>
    /// A weak event handler wrapper for events based on the commonly used EventHandler protocol.
    /// </summary>
    public sealed class WeakEventHandler
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakEventHandler(EventHandler callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public void Handler(object sender, EventArgs e)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action<object, EventArgs>)Delegate.CreateDelegate(typeof(Action<object, EventArgs>), target, _method, true);
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
        }
    }

    /// <summary>
    /// A weak event handler wrapper for events based on the generic EventHandler<TArgs/> protocol.
    /// </summary>
    public sealed class WeakEventHandler<TEventArgs> where TEventArgs : EventArgs
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakEventHandler(EventHandler<TEventArgs> callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public void Handler(object sender, TEventArgs e)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action<object, TEventArgs>)Delegate.CreateDelegate(typeof(Action<object, TEventArgs>), target, _method, true);
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
        }
    }

    /// <summary>
    /// A weak event handler wrapper for collection changed events based on the NotifyCollectionChangedEventArgs protocol.
    /// </summary>
    public sealed class WeakCollectionChangedEventHandler
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakCollectionChangedEventHandler(NotifyCollectionChangedEventHandler callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public void Handler(object sender, NotifyCollectionChangedEventArgs e)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action<object, NotifyCollectionChangedEventArgs>)Delegate.CreateDelegate(typeof(Action<object, NotifyCollectionChangedEventArgs>), target, _method, true);
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
        }
    }

    /// <summary>
    /// A weak event handler wrapper for whatever generic handler protocol that takes no args and returns a value.
    /// </summary>
    public sealed class WeakFuncHandler<TResult>
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakFuncHandler(Func<TResult> callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public TResult Handler()
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Func<TResult>)Delegate.CreateDelegate(typeof(Func<TResult>), target, _method, true);
                if (callback != null)
                {
                    return callback();
                }
            }
            return default(TResult);
        }
    }

    /// <summary>
    /// A weak event handler wrapper for whatever generic handler protocol that takes a single arg and returns a value.
    /// </summary>
    public sealed class WeakFuncHandler<TArg, TResult>
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakFuncHandler(Func<TArg, TResult> callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public TResult Handler(TArg arg)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Func<TArg, TResult>)Delegate.CreateDelegate(typeof(Func<TArg, TResult>), target, _method, true);
                if (callback != null)
                {
                    return callback(arg);
                }
            }
            return default(TResult);
        }
    }
    
    /// <summary>
    /// A weak action handler wrapper that takes no args.
    /// </summary>
    public sealed class WeakActionHandler
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakActionHandler(Action callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public void Handler()
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action)Delegate.CreateDelegate(typeof(Action), target, _method, true);
                if (callback != null)
                {
                    callback();
                }
            }
        }
    }
    
    /// <summary>
    /// A weak action handler wrapper that takes 1 arg.
    /// </summary>
    public sealed class WeakActionHandler<TArg>
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakActionHandler(Action<TArg> callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        public void Handler(TArg arg)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action<TArg>)Delegate.CreateDelegate(typeof(Action<TArg>), target, _method, true);
                if (callback != null)
                {
                    callback(arg);
                }
            }
        }
    }
}

