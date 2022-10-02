using System;
using System.Collections.Generic;

namespace NeCo.Recursion
{
    internal class RecursionResolver : INeCoResolver
    {
        private ProviderCaches caches = null;

        internal void SetCaches(ProviderCaches caches)
        {
            this.caches = caches;

            var entryPoints = caches.GetEntryPoints();

            foreach (var point in entryPoints)
                point.Provide(new ProviderCaches(), caches);
        }

        public object Resolve(Type type, string id)
        {
            if (this.caches == null)
            {
                return null;
            }

            var cache = caches.Get(type, id);
            var implement = cache.Provide(new ProviderCaches(), caches);

            return implement;
        }

        public void Dispose()
        {
            if (this.caches != null)
            {
                this.caches.Dispose();
                this.caches = null;
            }
        }
    }
}