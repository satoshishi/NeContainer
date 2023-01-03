using System;
using System.Collections.Concurrent;

namespace NeCo
{
    internal static class InjecterContainer
    {
        private static ConcurrentDictionary<Type, INeCoInjecter> container = new ConcurrentDictionary<Type, INeCoInjecter>();

        internal static bool TryGet(Type key, out INeCoInjecter target)
        {
            if (container.ContainsKey(key))
            {
                target = container[key];
                return true;
            }

            target = null;
            return false;
        }

        internal static void Add(Type key, INeCoInjecter value)
        {
            container.TryAdd(key, value);
        }
    }
}