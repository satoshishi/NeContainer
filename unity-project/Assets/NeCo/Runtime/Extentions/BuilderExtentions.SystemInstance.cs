using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
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

        #region fromto singleton

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder, bool isThisEntryPoint)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder, string id)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, false, id);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder, bool isThisEntryPoint, string id)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, isThisEntryPoint, id);
            return parameter;
        }

        #endregion

        #region fromto transient

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder, string id)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, false, id);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder, bool isThisEntryPoint)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROMTO">登録するクラスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder, bool isThisEntryPoint, string id)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, isThisEntryPoint, id);
            return parameter;
        }

        #endregion

        #region fromto constant

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance, string id)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, false, id);
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance, bool isThisEntryPoint)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance, bool isThisEntryPoint, string id)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, isThisEntryPoint, id);
            return parameter;
        }

        #endregion

        #region from,to singleton

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder, bool isThisEntryPoint)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder, string id)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, false, id);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder, bool isThisEntryPoint, string id)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, isThisEntryPoint, id);
            return parameter;
        }

        #endregion

        #region from,to transient

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder, bool isThisEntryPoint = false)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder, string id)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, false, id);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, false, string.Empty);
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <typeparam name="TO">Resolveの結果として返すインスタンスの型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder, bool isThisEntryPoint, string id)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, isThisEntryPoint, id);
            return parameter;
        }

        #endregion

        #region from,to constant

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance, bool isThisEntryPoint = false)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, isThisEntryPoint, string.Empty);
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance, string id)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, false, id);
            return parameter;
        }        

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, false, string.Empty);
            return parameter;
        }                

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        /// <param name="builder">コンテナを生成するビルダー</param>
        /// <param name="instance">登録する生成済みのインスタンス</param>
        /// <param name="isThisEntryPoint">このクラスのインスタンスをBuild後に生成するか</param>
        /// <param name="id">id</param>
        /// <typeparam name="FROM">Resolverから受けとつける型</typeparam>
        /// <returns></returns>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance, bool isThisEntryPoint, string id)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, isThisEntryPoint, id);
            return parameter;
        }        

        #endregion

        private static IRegistrationParamter Registration(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object instance, bool isThisEntryPoint, string id)
        {
            var info = CreateSystemInstanceInfo(
                from: new Dependencys(from, id),
                to: to,
                instanceType: instanceType,
                isThisEntryPoint: isThisEntryPoint,
                instance: instance
            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreateSystemInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object instance = null, bool isThisEntryPoint = false)
        {
            if (instance != null && instance.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承しているクラスを指定しました : " + instance.GetType());

            INeCoInjecter injecter = CreateInjecter(to, false);

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
    }
}