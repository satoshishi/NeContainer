using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo
{
    public static class ReflectionExtentions
    {
        internal static bool IsMonoBehaviourSubClass(this Type target)
        {
            return target.IsSubclassOf(typeof(MonoBehaviour));
        }

        internal static bool HasConstructor(this Type target, out ConstructorInfo constructorInfo)
        {
            var constructors = target.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);;
            constructorInfo = null;

            if(constructors != null && constructors.Length > 0)
            {
                constructorInfo = constructors[0];
                return true;
            }

            return false;
        }        

        internal static bool HasInjectionAttributeInAny(this Type target)
        {
            var members = target.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach(var member in members)
            {
                bool injectAttribute = member.GetCustomAttribute<InjectAttribute>() != null;
                bool injectFromIDAttribute = member.GetCustomAttribute<InjectFromIDAttribute>() != null;

                if(injectAttribute || injectFromIDAttribute)
                {
                    return true;
                }
            }

            return false;
        }        

        internal static bool HasInjectionAttributeInConstructor(this Type target, out ConstructorInfo constructorInfo)
        {
            var constructors = target.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            constructorInfo = null;

            foreach (var constructor in constructors)
            {
                var injection = constructor.GetCustomAttribute<InjectAttribute>();
                if (injection != null)
                {
                    constructorInfo = constructor;
                    return true;
                }
            }
            return false;
        }

        internal static bool HasInjectionAttributeInProperty(this Type target, out (PropertyInfo, string)[] PropertyInfos)
        {
            var propertys = target.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            List<(PropertyInfo, string)> targets = new List<(PropertyInfo, string)>();

            foreach (var property in propertys)
            {
                if (property == null)
                    continue;

                var defalutInject = property.GetCustomAttribute<InjectAttribute>();
                if (defalutInject != null)
                    targets.Add((property, ""));
                else
                {
                    var idInject = property.GetCustomAttribute<InjectFromIDAttribute>();
                    if (idInject != null)
                        targets.Add((property, idInject.id));
                }
            }

            PropertyInfos = targets.ToArray();
            return targets.Count > 0;
        }

        internal static bool HasInjectionAttributeInMethod(this Type target, out MethodInfo methodInfo)
        {
            var methods = target.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            methodInfo = null;

            foreach (var method in methods)
            {
                var injection = method.GetCustomAttribute<InjectAttribute>();
                if (injection != null)
                {
                    methodInfo = method;
                    return true;
                }
            }
            return false;
        }
    }
}