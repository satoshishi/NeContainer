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
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Constant, instance, null, false, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="options">登録する際のoption</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, keyValue.Item1, keyValue.Item1, InstanceType.Constant, keyValue.Item2, options.Parent, options.DontDestoryOnLoad, options.IsThisEntryPoint, options.Id);
            return parameter;
        }


        #endregion

        #region simply transient

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Transient, instance, null, false, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="options">登録する際のoption</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, keyValue.Item1, keyValue.Item1, InstanceType.Transient, keyValue.Item2, options.Parent, options.DontDestoryOnLoad, options.IsThisEntryPoint, options.Id);
            return parameter;
        }

        #endregion

        #region from singleton

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Constant, instance, null, false, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="options">登録する際のoption</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), keyValue.Item1, InstanceType.Constant, keyValue.Item2, options.Parent, options.DontDestoryOnLoad, options.IsThisEntryPoint, options.Id);
            return parameter;
        }

        #endregion

        #region from transient

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient<FROM>(this INeCoBuilder builder, MonoBehaviour prefab)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Transient, instance, null, false, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="options">登録する際のoption</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, PrefabRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, prefab);

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), keyValue.Item1, InstanceType.Transient, keyValue.Item2, options.Parent, options.DontDestoryOnLoad, options.IsThisEntryPoint, options.Id);
            return parameter;
        }

        #endregion

        private static IRegistrationParamter RegistrationPrefab(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, Transform parent, bool dontDestoryOnLoad, bool isThisEntryPoint, string id)
        {
            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(from, id),
                to: to,
                instanceType: instanceType,
                gameObject: gameObject,
                parent: parent,
                dontDestoryOnLoad: dontDestoryOnLoad,
                isThisEntryPoint: isThisEntryPoint

            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreatePrefabInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isThisEntryPoint = false)
        {
            INeCoInjecter injecter = CreateInjecter(to, true);

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
    }
}