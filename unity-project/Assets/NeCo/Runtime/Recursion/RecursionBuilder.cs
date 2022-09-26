using System;

namespace NeCo.Recursion
{
    internal sealed class ParamterCaches : Caches<Dependencys, IRegistrationParamter>, IDisposable
    {
        public override bool Match(Dependencys key, out IRegistrationParamter value)
        {
            if (this.caches != null)
            {
                foreach (var cache in caches)
                {
                    var target = cache.From;

                    foreach (var k in key.Sources)
                    {
                        if (target.Match((k.Type, k.Id), out Dependencys.Source source))
                        {
                            value = cache;
                            return true;
                        }
                    }
                }
            }

            value = null;
            return false;
        }

        public IRegistrationParamter[] Parameters => caches;

        public void Dispose()
        {
            if (this.caches != null)
            {
                foreach (IRegistrationParamter parameter in this.caches)
                {
                    parameter.Dispose();
                }

                this.caches = null;
            }
        }
    }

    internal class RecursionBuilder : INeCoBuilder
    {
        private ParamterCaches caches = new ParamterCaches();

        public bool Vaild => this.vaild;
        private bool vaild = true;

        internal RecursionBuilder()
        {
            this.vaild = true;
        }

        public IRegistrationParamter Register(IRegistrationParamter info)
        {
            if (!this.vaild)
            {
                return default;
            }

            caches.Add(info.From, info);
            return info;
        }

        public INeCoResolver Build()
        {
            ProviderCaches providers = new ProviderCaches();
            RecursionResolver resolver = new RecursionResolver();

            if (this.vaild)
            {
                providers.Add(new ResolverInstanceProvider(resolver));
                foreach (var parameter in caches.Parameters)
                    providers.Add(parameter.CreateProvider());

                resolver.SetCaches(providers);
            }

            return resolver;
        }

        public void Dispose()
        {
            this.vaild = false;

            if (this.caches != null)
            {
                this.caches.Dispose();
                this.caches = null;
            }
        }
    }
}