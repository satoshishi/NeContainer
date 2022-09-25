using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {
        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton(this INeCoBuilder builder, MonoBehaviour prefab, Transform parent, bool isThisEntryPoint = false, bool dontDestoryOnLoad = false)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Constant, instance, parent, dontDestoryOnLoad, isThisEntryPoint);
            return parameter;
        }        

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient(this INeCoBuilder builder, MonoBehaviour prefab, Transform parent, bool isThisEntryPoint = false, bool dontDestoryOnLoad = false)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, type, type, InstanceType.Transient, instance, parent, dontDestoryOnLoad, isThisEntryPoint);
            return parameter;
        }                

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve時に生成し、以降の参照は共通のインスタンスを提供する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, Transform parent, bool isThisEntryPoint = false, bool dontDestoryOnLoad = false)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Constant, instance, parent, dontDestoryOnLoad, isThisEntryPoint);
            return parameter;
        }

        /// <summary>
        /// Prefabをコンテナに登録する
        /// Resolve毎にInstantiateを実行する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="prefab">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationPrefab_AsTransient<FROM>(this INeCoBuilder builder, MonoBehaviour prefab, Transform parent, bool isThisEntryPoint = false, bool dontDestoryOnLoad = false)
        {
            Type type = prefab.GetType();
            object instance = prefab;

            IRegistrationParamter parameter = RegistrationPrefab(builder, typeof(FROM), type, InstanceType.Transient, instance, parent, dontDestoryOnLoad, isThisEntryPoint);
            return parameter;
        }                    

        private static IRegistrationParamter RegistrationPrefab(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isThisEntryPoint = false)
        {
            var info = CreatePrefabInstanceInfo(
                from: new Dependencys(from, string.Empty),
                to: to,
                instanceType: instanceType,
                gameObject: gameObject,
                parent: parent,
                dontDestoryOnLoad : dontDestoryOnLoad,
                isThisEntryPoint: isThisEntryPoint

            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreatePrefabInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, Transform parent = null, bool dontDestoryOnLoad = false, bool isThisEntryPoint = false)
        {
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
    }
}