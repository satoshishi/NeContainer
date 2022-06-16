using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static class BuilderExtentions
    {

        public static IRegistrationParamter Or<FROM>(this IRegistrationParamter parameter, string id = "")
        {
            parameter.From.Add(typeof(FROM), id);

            return parameter;
        }

        public static IRegistrationParamter Or<FROM>(this IRegistrationParamter parameter)
        {
            parameter.From.Add(typeof(FROM), "");

            return parameter;
        }

        public static IRegistrationParamter Register<FROMTO>(this INeCoBuilder builder, InstanceType instanceType)
        {
            return builder.Register<FROMTO>(instanceType, false, "");
        }

        public static IRegistrationParamter Register<FROMTO>(this INeCoBuilder builder, InstanceType instanceType, string id = "")
        {
            return builder.Register<FROMTO>(instanceType, false, id);
        }

        public static IRegistrationParamter Register<FROMTO>(this INeCoBuilder builder, InstanceType instanceType, bool isThisEntryPoint = false)
        {
            return builder.Register<FROMTO>(instanceType, isThisEntryPoint, "");
        }

        public static IRegistrationParamter Register<FROMTO>(this INeCoBuilder builder, InstanceType instanceType, bool isThisEntryPoint = false, string id = "")
        {
            if (instanceType == InstanceType.Constant)
                throw new NotSupportedException("Constantとして取り扱うインスタンスの指定がありません : " + typeof(FROMTO));

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

        public static IRegistrationParamter Register<FROM, TO>(this INeCoBuilder builder, InstanceType instanceType)
        {
            return builder.Register<FROM, TO>(instanceType, false, "");
        }

        public static IRegistrationParamter Register<FROM, TO>(this INeCoBuilder builder, InstanceType instanceType, string id)
        {
            return builder.Register<FROM, TO>(instanceType, false, id);
        }

        public static IRegistrationParamter Register<FROM, TO>(this INeCoBuilder builder, InstanceType instanceType, bool isThisEntryPoint = false)
        {
            return builder.Register<FROM, TO>(instanceType, isThisEntryPoint, "");
        }

        public static IRegistrationParamter Register<FROM, TO>(this INeCoBuilder builder, InstanceType instanceType, bool isThisEntryPoint = false, string id = "")
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

        public static IRegistrationParamter Register(this INeCoBuilder builder, object instance)
        {
            return builder.Register(instance, false, "");
        }

        public static IRegistrationParamter Register(this INeCoBuilder builder, object instance, bool isThisEntryPoint = false)
        {
            return builder.Register(instance, isThisEntryPoint, "");
        }

        public static IRegistrationParamter Register(this INeCoBuilder builder, object instance, string id = "")
        {
            return builder.Register(instance, false, id);
        }        

        public static IRegistrationParamter Register(this INeCoBuilder builder, object instance, bool isThisEntryPoint = false, string id = "")
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

        public static IRegistrationParamter Register<FROM>(this INeCoBuilder builder, object instance)
        {
            return builder.Register<FROM>(instance, false, "");
        }

        public static IRegistrationParamter Register<FROM>(this INeCoBuilder builder, object instance, bool isThisEntryPoint = false)
        {
            return builder.Register<FROM>(instance, isThisEntryPoint, "");
        }

        public static IRegistrationParamter Register<FROM>(this INeCoBuilder builder, object instance, string id = "")
        {
            return builder.Register<FROM>(instance, false, id);
        }        

        public static IRegistrationParamter Register<FROM>(this INeCoBuilder builder, object instance, bool isThisEntryPoint = false, string id = "")
        {
            var info = CreateSystemInstanceInfo(
                from: new Dependencys(typeof(FROM), id),
                to: instance.GetType(),
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                instance: instance
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterMonoBehaviour(this INeCoBuilder builder, object gameObject)
        {
            return builder.RegisterMonoBehaviour(gameObject, false, "");
        }

        public static IRegistrationParamter RegisterMonoBehaviour(this INeCoBuilder builder, object gameObject, bool isThisEntryPoint = false)
        {
            return builder.RegisterMonoBehaviour(gameObject, isThisEntryPoint, "");
        }

        public static IRegistrationParamter RegisterMonoBehaviour(this INeCoBuilder builder, object instance, string id = "")
        {
            return builder.Register(instance, false, id);
        }                

        public static IRegistrationParamter RegisterMonoBehaviour(this INeCoBuilder builder, object gameObject, bool isThisEntryPoint = false, string id = "")
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
        public static IRegistrationParamter RegisterMonoBehaviour<FROM>(this INeCoBuilder builder, object gameObject)
        {
            return builder.RegisterMonoBehaviour<FROM>(gameObject, false, "");
        }

        public static IRegistrationParamter RegisterMonoBehaviour<FROM>(this INeCoBuilder builder, object gameObject, bool isThisEntryPoint = false)
        {
            return builder.RegisterMonoBehaviour<FROM>(gameObject, isThisEntryPoint, "");
        }

        public static IRegistrationParamter RegisterMonoBehaviour<FROM>(this INeCoBuilder builder, object instance, string id = "")
        {
            return builder.Register<FROM>(instance, false, id);
        }                

        public static IRegistrationParamter RegisterMonoBehaviour<FROM>(this INeCoBuilder builder, object gameObject, bool isThisEntryPoint = false, string id = "")
        {
            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(typeof(FROM), id),
                to: gameObject.GetType(),
                instanceType: InstanceType.Constant,
                isThisEntryPoint: isThisEntryPoint,
                gameObject: gameObject
            );

            builder.Register(info);
            return info;
        }

        public static IRegistrationParamter RegisterPrefab(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = false)
        {
            return builder.RegisterPrefab(gameObject, parent, dontDestoryOnLoad, isTransient, false, "");
        }

        public static IRegistrationParamter RegisterPrefab(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = false, bool isThisEntryPoint = false)
        {
            return builder.RegisterPrefab(gameObject, parent, dontDestoryOnLoad, isTransient, isThisEntryPoint, "");
        }

        public static IRegistrationParamter RegisterPrefab(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = true,  bool isThisEntryPoint = false, string id = "")
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

        public static IRegistrationParamter RegisterPrefab<FROM>(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = false)
        {
            return builder.RegisterPrefab<FROM>(gameObject, parent, dontDestoryOnLoad, isTransient, false, "");
        }

        public static IRegistrationParamter RegisterPrefab<FROM>(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = false, bool isThisEntryPoint = false)
        {
            return builder.RegisterPrefab<FROM>(gameObject, parent, dontDestoryOnLoad, isTransient, isThisEntryPoint, "");
        }

        public static IRegistrationParamter RegisterPrefab<FROM>(this INeCoBuilder builder, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isTransient = true,  bool isThisEntryPoint = false, string id = "")
        {
            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(typeof(FROM), id),
                to: gameObject.GetType(),
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

            //if (target.HasConstructor(out constructor))
            //return new ConstructInjecter(constructor);                

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
                return new MethodInjecter(method);

            if (target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
                return new PropertyInjecter(propertys);

            return new DoNotInjecter();
        }

        private static IRegistrationParamter CreateMonoBehaviourInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, bool isThisEntryPoint = false)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました : " + gameObject.GetType());

            INeCoInjecter injecter = CreateInjecter(to);

            return new MonoBehaviourInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject as MonoBehaviour,
                isThisEntryPoint
            );
        }

        private static IRegistrationParamter CreatePrefabInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isThisEntryPoint = false)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました : " + gameObject.GetType());

            INeCoInjecter injecter = CreateInjecter(to);

            return new PrefabInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject as MonoBehaviour,
                parent,
                dontDestoryOnLoad,
                isThisEntryPoint
            );
        }

        private static IRegistrationParamter CreateSystemInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object instance = null, bool isThisEntryPoint = false)
        {
            if (instance != null && instance.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承しているクラスを指定しました : "  + instance.GetType());

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