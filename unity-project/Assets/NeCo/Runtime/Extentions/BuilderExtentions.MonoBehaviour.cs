using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <returns></returns>        
        public static IRegistrationParamter RegistrationMonoBehaviour_AsConstant(this INeCoBuilder builder, MonoBehaviour gameObject, bool isThisEntryPoint = false)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, type, type, InstanceType.Constant, instance, isThisEntryPoint);
            return parameter;
        }

        /// <summary>
        /// 既に生成したMonoBehaivourを継承したクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="gameObject">クラス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationMonoBehaviour_AsConstant<FROM>(this INeCoBuilder builder, MonoBehaviour gameObject, bool isThisEntryPoint = false)
        {
            Type type = gameObject.GetType();
            object instance = gameObject;

            IRegistrationParamter parameter = RegistrationMonoBehaviour(builder, typeof(FROM), type, InstanceType.Constant, instance, isThisEntryPoint);
            return parameter;
        }

        private static IRegistrationParamter RegistrationMonoBehaviour(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object gameObject, bool isThisEntryPoint = false)
        {
            var info = CreateMonoBehaviourInstanceInfo(
                from: new Dependencys(from, string.Empty),
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
    }
}