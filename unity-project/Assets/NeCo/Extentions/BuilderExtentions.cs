using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static class BuilderExtentions
    {

        public static IRegistrationParamter As<TO>(this IRegistrationParamter parameter, string id = "")
        {
            parameter.From.Add(typeof(TO),id);

            return parameter;
        }

        public static IRegistrationParamter Register<FROMTO>(this INeCoBuilder builder, InstanceType instanceType, string id = "", bool isThisEntryPoint = false)
        {
            if (instanceType == InstanceType.Constant)
                throw new NotSupportedException("Constantとして取り扱うインスタンスの指定がありません");

            Type type = typeof(FROMTO);

            var info = CreateSystemInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: instanceType,
                isThisEntryPoint: isThisEntryPoint
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter Register<FROM, TO>(this INeCoBuilder builder, InstanceType instanceType, string id = "", bool isThisEntryPoint = false)
        {
            if (instanceType == InstanceType.Constant)
                throw new NotSupportedException("Constantとして取り扱うインスタンスの指定がありません");

            var info = CreateSystemInstanceInfo(
                from: new Dependencys(typeof(FROM), id),
                to: typeof(TO),
                instanceType: instanceType,
                isThisEntryPoint: isThisEntryPoint
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter Register(this INeCoBuilder builder, object instance, string id = "", bool isThisEntryPoint = false)
        {
            Type type = instance.GetType();

            var info = CreateSystemInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                instance: instance
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter Register<TO>(this INeCoBuilder builder, object instance, string id = "", bool isThisEntryPoint = false)
        {
            var info = CreateSystemInstanceInfo(
                from: new Dependencys(instance.GetType(), id),
                to: typeof(TO),
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                instance: instance
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterMonoBehaviour(this INeCoBuilder builder, MonoBehaviour gameObject, string id = "", bool isThisEntryPoint = false)
        {
            Type type = gameObject.GetType();

            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                gameObject: gameObject
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterMonoBehaviour<TO>(this INeCoBuilder builder, MonoBehaviour gameObject, string id = "", bool isThisEntryPoint = false)
        {
            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(gameObject.GetType(), id),
                to: typeof(TO),
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                gameObject: gameObject
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterPrefab(this INeCoBuilder builder, MonoBehaviour gameObject, Transform parent, bool dontDestoryOnLoad, bool isTransient, string id = "", bool isThisEntryPoint = false)
        {
            Type type = gameObject.GetType();

            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: isTransient ? InstanceType.Transient : InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                gameObject: gameObject,
                dontDestoryOnLoad: dontDestoryOnLoad,
                parent: parent
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterPrefab<TO>(this INeCoBuilder builder, MonoBehaviour gameObject, Transform parent, bool dontDestoryOnLoad, bool isTransient, string id = "", bool isThisEntryPoint = false)
        {
            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(gameObject.GetType(), id),
                to: typeof(TO),
                instanceType: isTransient ? InstanceType.Transient : InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                gameObject: gameObject,
                dontDestoryOnLoad: dontDestoryOnLoad,
                parent: parent
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterFunc<INPUT, OUTPUT>(this INeCoBuilder builder, Func<INPUT, OUTPUT> func, string id = "", bool isThisEntryPoint = false)
        {
            var type = typeof(Func<INPUT, OUTPUT>);

            var info = CreateSystemInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                instance: func
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterFunc<INPUT, OUTPUT>(this INeCoBuilder builder, Func<INeCoResolver, Func<INPUT, OUTPUT>> func, string id = "", bool isThisEntryPoint = false)
        {
            Type type = typeof(Func<INeCoResolver, Func<INPUT, OUTPUT>>);

            var info = CreateSystemInstanceInfo(
                from: new Dependencys(type, id),
                to: type,
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                instance: func
            );

            builder.Register(info);
            return info;
        }

        #region factory

        private static INeCoInjecter CreateInjecter(Type target)
        {
            if (target.HasInjectionAttributeInConstructor(out ConstructorInfo constructor))
                return new ConstructInjecter(constructor);

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
                return new MethodInjecter(method);

            if (target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
                return new PropertyInjecter(propertys);

            return new DoNotInjecter();
        }

        private static IRegistrationParamter CreateMonoBehaviourInstanceInfo(Dependencys from, Type to, InstanceType instanceType, MonoBehaviour gameObject, bool isThisEntryPoint = false)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました");

            INeCoInjecter injecter = CreateInjecter(to);

            return new MonoBehaviourInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject,
                isThisEntryPoint
            );
        }

        private static IRegistrationParamter CreatePrefabInstanceInfo(Dependencys from, Type to, InstanceType instanceType, MonoBehaviour gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isThisEntryPoint = false)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました");

            INeCoInjecter injecter = CreateInjecter(to);

            return new PrefabInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject,
                parent,
                dontDestoryOnLoad,
                isThisEntryPoint
            );
        }        

        private static IRegistrationParamter CreateSystemInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object instance = null, bool isThisEntryPoint = false)
        {
            if (instance != null && instance.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承しているクラスを指定しました");

            INeCoInjecter injecter = CreateInjecter(to);

            return new SystemInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                instance,
                isThisEntryPoint
            );
        }

        #endregion
    }
}