using System;
using System.Collections.Generic;
using System.Reflection;

namespace NeCo.Recursion
{
    internal abstract class RecursionInjecter : INeCoInjecter
    {
        public Dependencys InjectionTarget
        {
            get => injectionTarget;
        }
        protected Dependencys injectionTarget = null;

        public object Inject(object instance, ProviderCaches history, ProviderCaches caches)
        {
            //コンストラクタで依存している参照を取得しに行く
            List<object> args = new List<object>();
            foreach (var aug in injectionTarget.Sources)
            {
                var cache = caches.Get(aug);
                object result = null;

                //循環参照が発生している場合
                if (history.Match(aug, out NeCoProvider patrolled))
                    result = ResolveCircularReference(patrolled, history, caches);
                //循環参照が発生していない場合は、再起的にinjectするインスタンスを生成する
                else result = cache.Provide(history, caches);

                args.Add(result);
            }
            return CreateInstance(instance, args, injectionTarget);
        }

        public object ResolveCircularReference(NeCoProvider target, ProviderCaches history, ProviderCaches caches)
        {
            return target.Info.IsSingleton() || target.Info.IsConstant() ? target.Provide(history, caches) : null;
        }

        internal abstract object CreateInstance(object instance, List<object> args, Dependencys parameter);

        public virtual void Dispose() { }
    }

    internal sealed class DoNotInjecter : RecursionInjecter
    {
        internal override object CreateInstance(object instance, List<object> args, Dependencys parameter)
        {
            return instance;
        }

        public DoNotInjecter()
        {
            injectionTarget = new Dependencys();
        }
    }

    internal sealed class FunctionInjecter : RecursionInjecter
    {
        internal override object CreateInstance(object instance, List<object> args, Dependencys parameter)
        {
            INeCoResolver resolver = args[0] as INeCoResolver;
            Func<INeCoResolver, object> func = instance as Func<INeCoResolver, object>;

            return func.Invoke(resolver);
        }        
        public FunctionInjecter()
        {
            injectionTarget = new Dependencys();            

            var source = new Dependencys.Source(typeof(INeCoResolver), "");
            injectionTarget.Add((source.Type, source.Id), source);            
        }
    }

    internal sealed class ConstructInjecter : RecursionInjecter
    {
        private ConstructorInfo constructor = null;

        internal override object CreateInstance(object instance, List<object> args, Dependencys parameter)
        {
            constructor.Invoke(instance, args.ToArray());

            return instance;
        }

        public ConstructInjecter(ConstructorInfo constructor)
        {
            this.constructor = constructor;

            injectionTarget = new Dependencys();
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                var source = new Dependencys.Source(parameter.ParameterType, "");
                injectionTarget.Add((source.Type, source.Id), source);
            }
        }
    }

    internal sealed class PropertyInjecter : RecursionInjecter
    {
        private (PropertyInfo, string)[] propertys = null;

        internal override object CreateInstance(object instance, List<object> args, Dependencys parameter)
        {
            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].Item1.CanWrite)
                    propertys[i].Item1.SetValue(instance, args[i]);
            }

            return instance;
        }

        public PropertyInjecter((PropertyInfo, string)[] propertys)
        {
            this.propertys = propertys;

            injectionTarget = new Dependencys();
            foreach (var parameter in propertys)
            {
                var source = new Dependencys.Source(parameter.Item1.PropertyType, parameter.Item2);
                injectionTarget.Add((source.Type, source.Id), source);
            }
        }
    }

    internal sealed class MethodInjecter : RecursionInjecter
    {
        private MethodInfo method = null;

        internal override object CreateInstance(object instance, List<object> args, Dependencys parameter)
        {
            method.Invoke(instance, args.ToArray());

            return instance;
        }

        public MethodInjecter(MethodInfo method)
        {
            this.method = method;

            injectionTarget = new Dependencys();
            var parameters = method.GetParameters();
            foreach (var parameter in parameters)
            {
                var source = new Dependencys.Source(parameter.ParameterType, "");
                injectionTarget.Add((source.Type, source.Id), source);
            }
        }
    }
}