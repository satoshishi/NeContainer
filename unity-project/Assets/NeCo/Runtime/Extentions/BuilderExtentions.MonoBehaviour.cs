using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {

        #region simply constant

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <param name="options">登録する際のoptions</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton(this INeCoBuilder builder, MonoBehaviour gameObject, MonoBehaviourRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, gameObject);

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, keyValue.Item1, keyValue.Item1, InstanceType.Constant, keyValue.Item2, options.IsThisEntryPoint, options.Id);
            return parameter;
        }

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton(this INeCoBuilder builder, MonoBehaviour gameObject)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, type, type, InstanceType.Constant, instance, false, string.Empty);
            return parameter;
        }

        #endregion

        #region from constant

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour gameObject, MonoBehaviourRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, gameObject);

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, typeof(FROM), keyValue.Item1, InstanceType.Constant, keyValue.Item2, options.IsThisEntryPoint, options.Id);
            return parameter;
        }

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour gameObject)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, typeof(FROM), type, InstanceType.Constant, instance, false, string.Empty);
            return parameter;
        }

        #endregion

        private static IRegistrationParamter RegistrationMonoBehaviour(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, bool isThisEntryPoint, string id)
        {
            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(from, id),
                to: to,
                instanceType: instanceType,
                gameObject: gameObject,
                isThisEntryPoint: isThisEntryPoint
            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreateMonoBehaviourInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, bool isThisEntryPoint = false)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました : " + gameObject.GetType());

            INeCoInjecter injecter = CreateInjecter(to, true);

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
    }
}