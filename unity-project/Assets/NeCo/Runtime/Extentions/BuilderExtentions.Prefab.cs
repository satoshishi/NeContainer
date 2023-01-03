using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {
        #region simply singleton

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        public static IRegistrationParamter RegistrationPrefab_AsSingleton(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Constant, instance, new PrefabRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        public static IRegistrationParamter RegistrationPrefab_AsSingleton(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, keyValue.Item1, keyValue.Item1, InstanceType.Constant, keyValue.Item2, options);
            return parameter;
        }


        #endregion

        #region simply transient

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        public static IRegistrationParamter RegistrationPrefab_AsTransient(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Transient, instance, new PrefabRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>  
        public static IRegistrationParamter RegistrationPrefab_AsTransient(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, keyValue.Item1, keyValue.Item1, InstanceType.Transient, keyValue.Item2, options);
            return parameter;
        }

        #endregion

        #region from singleton

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        public static IRegistrationParamter RegistrationPrefab_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Constant, instance, new PrefabRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>       
        public static IRegistrationParamter RegistrationPrefab_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), keyValue.Item1, InstanceType.Constant, keyValue.Item2, options);
            return parameter;
        }

        #endregion

        #region from transient

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>   
        public static IRegistrationParamter RegistrationPrefab_AsTransient<FROM>(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Transient, instance, new PrefabRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>   
        public static IRegistrationParamter RegistrationPrefab_AsTransient<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), keyValue.Item1, InstanceType.Transient, keyValue.Item2, options);
            return parameter;
        }

        #endregion

        private static IRegistrationParamter RegistrationPrefab(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, PrefabRegistrationOptions options)
        {
            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(from, options.Id),
                to: to,
                instanceType: instanceType,
                gameObject: gameObject,
                options
            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreatePrefabInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, PrefabRegistrationOptions options)
        {
            INeCoInjecter injecter = CreateInjecter(to, options.injectionType);

            return new PrefabInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject as MonoBehaviour,
                options.Parent,
                options.DontDestoryOnLoad,
                options.IsThisEntryPoint
            );
        }
    }
}