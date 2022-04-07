using System.Collections.Generic;
using System;

namespace NeCo.Recursion
{
    internal sealed class ParamterCaches : Caches<Dependencys, IRegistrationParamter>
    {
        public override bool Match(Dependencys key, out IRegistrationParamter value)
        {
            foreach (var cache in caches)
            {
                var target = cache.From;

                foreach(var k in key.Sources)
                {
                    if(target.Match((k.Type,k.Id),out Dependencys.Source source))
                    {
                        value = cache;
                        return true;
                    }
                }
            }

            value = null;
            return false;
        }

        public IRegistrationParamter[] Parameters => caches;
    }

    internal class RecursionBuilder : INeCoBuilder
    {
        private ParamterCaches caches = new ParamterCaches();

        public IRegistrationParamter Register(IRegistrationParamter info)
        {
            caches.Add(info.From,info);
            return info;
        }

        public INeCoResolver Build()
        {
            ProviderCaches providers = new ProviderCaches();
            RecursionResolver resolver = new RecursionResolver();

            providers.Add(new ResolverInstanceProvider(resolver));
            foreach(var parameter in caches.Parameters)
                providers.Add(parameter.CreateProvider());

            resolver.SetCaches(providers);
            return resolver;
        }
    }
}