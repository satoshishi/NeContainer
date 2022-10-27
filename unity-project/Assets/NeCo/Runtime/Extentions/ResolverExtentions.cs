namespace NeCo
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NeCo.Recursion;

    public static class ResolverExtentions
    {
        public static TO Resolve<TO>(this INeCoResolver resolver)
        {
            var to = resolver.Resolve(typeof(TO), "");
            return (TO)(to);
        }

        public static TO Resolve<TO>(this INeCoResolver resolver, string id)
        {
            var to = resolver.Resolve(typeof(TO), id);
            return (TO)(to);
        }

        public static TO Instantiate<TO>(this INeCoResolver resolver, MonoBehaviour prefab, Transform root) where TO : class
        {
            object InjectionMethod(MethodInfo method, object instance)
            {
                MethodInjecter injecter = new MethodInjecter(method);
                var dependencys = method.GetParameters();
                List<object> args = new List<object>();

                foreach (ParameterInfo d in dependencys)
                {
                    args.Add(resolver.Resolve(d.ParameterType, ""));
                }

                return injecter.CreateInstance(instance, args, null);
            }

            object InjectionPropertys((PropertyInfo, string)[] propertys, object instance)
            {
                PropertyInjecter injecter = new PropertyInjecter(propertys);
                List<object> args = new List<object>();

                foreach ((PropertyInfo, string) property in propertys)
                {
                    args.Add(resolver.Resolve(property.Item1.PropertyType, ""));
                }

                return injecter.CreateInstance(instance, args, null);
            }            

            Type target = prefab.GetType();
            object instance = GameObject.Instantiate(prefab, root);                

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
                return InjectionMethod(method, instance) as TO;
            
            if(target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
                return InjectionPropertys(propertys, instance) as TO;

            return instance as TO;
        }
    }
}