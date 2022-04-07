using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Linq;

namespace NeCo
{
    internal abstract class NeCoProvider
    {
        public abstract IRegistrationParamter Info
        {
            get;
        }

        public object Instance { get; protected set; } = null;

        public object Provide(ProviderCaches history, ProviderCaches caches)
        {
            if (GetInstance() != null)
                return Instance;

            //依存解決までの履歴にこのインスタンスを追加
            history.Add(this);

            //inject前のインスタンスを生成
            var newInstance = CreateInstance(history, caches);

            //キャッシュ
            SetInstance(newInstance);

            //インスタンスにinject
            newInstance = Info.Injecter.Inject(newInstance, history, caches);

            return newInstance;
        }

        protected abstract void SetInstance(object instance);

        protected abstract object GetInstance();

        protected abstract object CreateInstance(ProviderCaches history, ProviderCaches caches);

        public virtual void Dispose() { }
    }

    internal class ProviderCaches : Caches<Dependencys.Source, NeCoProvider>
    {
        public override bool Match(Dependencys.Source key, out NeCoProvider value)
        {
            foreach (var provider in caches)
            {
                if (provider.Info.From.Match((key.Type, key.Id), out Dependencys.Source source))
                {
                    value = provider;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public bool Match(Type type, string id, out NeCoProvider value)
        {
            foreach (var provider in caches)
            {
                if (provider.Info.From.Match((type, id), out Dependencys.Source source))
                {
                    value = provider;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public void Add(NeCoProvider value)
        {
            var newCaches = new NeCoProvider[caches.Length + 1];
            System.Array.Copy(caches, newCaches, caches.Length);
            newCaches[caches.Length] = value;

            caches = newCaches;
        }

        public NeCoProvider[] GetEntryPoints()
        {
            List<NeCoProvider> entryPoints = new List<NeCoProvider>();
            foreach (var provider in caches)
            {
                if (provider.Info.IsThisEntryPoint)
                    entryPoints.Add(provider);
            }

            return entryPoints.ToArray();
        }

        public NeCoProvider Get(Type type, string id)
        {
            if (Match(type, id, out NeCoProvider value))
                return value;

            throw new KeyNotFoundException();
        }
    }

    internal sealed class MonoBehaviourInstanceProvider : NeCoProvider
    {
        public override IRegistrationParamter Info => info;
        private MonoBehaviourInstanceParameter info;

        public MonoBehaviourInstanceProvider(MonoBehaviourInstanceParameter info)
        {
            this.info = info;
        }

        protected override object CreateInstance(ProviderCaches history, ProviderCaches caches)
        {
            return info.GameObject;
        }

        protected override void SetInstance(object instance)
        {
            Instance = instance;
        }

        protected override object GetInstance()
        {
            if (Instance != null && (info.IsSingleton() || info.IsConstant()))
                return Instance;
            return null;
        }
    }

    internal sealed class PrefabInstanceProvider : NeCoProvider
    {
        public override IRegistrationParamter Info => info;
        private PrefabInstanceParameter info;

        public PrefabInstanceProvider(PrefabInstanceParameter info)
        {
            this.info = info;
        }

        protected override object CreateInstance(ProviderCaches history, ProviderCaches caches)
        {
            if(info.IsConstant() && Instance != null)
                return Instance;

            return GameObject.Instantiate(info.GameObject,info.Parent);
        }

        protected override void SetInstance(object instance)
        {
            if ((info.IsConstant()))
                Instance = instance;
        }

        protected override object GetInstance()
        {
            if (Instance != null && (info.IsSingleton() || info.IsConstant()))
                return Instance;
            return null;
        }
    }    

    internal sealed class SystemInstanceProvider : NeCoProvider
    {
        public override IRegistrationParamter Info => info;
        private SystemInstanceParameter info;

        public SystemInstanceProvider(SystemInstanceParameter info)
        {
            this.info = info;
        }

        protected override object CreateInstance(ProviderCaches history, ProviderCaches caches)
        {
            return info.IsConstant() ? info.Instance : FormatterServices.GetUninitializedObject(Info.To);
        }

        protected override void SetInstance(object instance)
        {
            if ((info.IsSingleton() || info.IsConstant()))
                Instance = instance;
        }

        protected override object GetInstance()
        {
            if (Instance != null && (info.IsSingleton() || info.IsConstant()))
                return Instance;
            return null;
        }
    }

    internal sealed class ResolverInstanceProvider : NeCoProvider
    {
        public override IRegistrationParamter Info => info;
        private IRegistrationParamter info = null;

        public ResolverInstanceProvider(object resolver)
        {
            Instance = resolver;
            info = new SystemInstanceParameter(typeof(INeCoResolver));
        }

        protected override object CreateInstance(ProviderCaches history, ProviderCaches caches)
        {
            throw new NotImplementedException();
        }

        protected override void SetInstance(object instance)
        {
            throw new NotImplementedException();
        }

        protected override object GetInstance()
        {
            return Instance;
        }
    }
}