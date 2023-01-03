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
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton(this INeCoBuilder builder, MonoBehaviour gameObject, MonoBehaviourRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, gameObject);

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, keyValue.Item1, keyValue.Item1, InstanceType.Constant, keyValue.Item2, options);
            return parameter;
        }

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton(this INeCoBuilder builder, MonoBehaviour gameObject)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, type, type, InstanceType.Constant, instance, new MonoBehaviourRegistrationOptions());
            return parameter;
        }

        #endregion

        #region from constant

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour gameObject, MonoBehaviourRegistrationOptions options)
        {
            (Type, object) keyValue = GetRegistrationTypeAndObject(options.ComponentTypeName, gameObject);

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, typeof(FROM), keyValue.Item1, InstanceType.Constant, keyValue.Item2, options);
            return parameter;
        }

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsSingleton<FROM>(this INeCoBuilder builder, MonoBehaviour gameObject)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, typeof(FROM), type, InstanceType.Constant, instance, new MonoBehaviourRegistrationOptions());
            return parameter;
        }

        #endregion

        private static IRegistrationParamter RegistrationMonoBehaviour(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, MonoBehaviourRegistrationOptions options)
        {
            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(from, options.Id),
                to: to,
                instanceType: instanceType,
                gameObject: gameObject,
                options
            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreateMonoBehaviourInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object gameObject, MonoBehaviourRegistrationOptions options)
        {
            if (!gameObject.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承していないクラスを指定しました : " + gameObject.GetType());

            INeCoInjecter injecter = CreateInjecter(to, options.injectionType);

            return new MonoBehaviourInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                gameObject as MonoBehaviour,
                options.IsThisEntryPoint
            );
        }
    }
}