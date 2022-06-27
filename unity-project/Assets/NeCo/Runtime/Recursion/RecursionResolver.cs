using System;
using System.Collections.Generic;

namespace NeCo.Recursion
{
    internal class RecursionResolver : INeCoResolver
    {
        private ProviderCaches caches = null;

        public void SetCaches(ProviderCaches caches)
        {
            this.caches = caches;

            var entryPoints = caches.GetEntryPoints();

            foreach (var point in entryPoints)
                point.Provide(new ProviderCaches(), caches);
        }

        public object Resolve(Type type, string id)
        {
            var cache = caches.Get(type, id);
            var implement = cache.Provide(new ProviderCaches(), caches);

            return implement;
        }
    }
}