using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo
{
    internal abstract class Caches<KEY, VALUE> where VALUE : class
    {
        protected VALUE[] caches = new VALUE[]{};

        public virtual void Add(KEY key, VALUE value)
        {
            if (!Match(key, out VALUE _value))
            {
                var newCaches = new VALUE[caches.Length + 1];
                System.Array.Copy(caches, newCaches, caches.Length);
                newCaches[caches.Length] = value;

                caches = newCaches;
            }
        }

        public abstract bool Match(KEY key, out VALUE value);

        public VALUE Get(KEY key)
        {
            if (Match(key, out VALUE value))
                return value;

            throw new KeyNotFoundException();
        }
    }
}