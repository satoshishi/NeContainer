using System;

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
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsSingleton<FROMTO>(this INeCoBuilder builder, SystemInstanceRegistrationOptions options)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Singleton, null, options);
            return parameter;
        }        

        #endregion

        #region fromto transient

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsTransient<FROMTO>(this INeCoBuilder builder, SystemInstanceRegistrationOptions options)
        {
            Type type = typeof(FROMTO);

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Transient, null, options);
            return parameter;
        }

        #endregion

        #region fromto constant

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsConstant(this INeCoBuilder builder, object instance, SystemInstanceRegistrationOptions options)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, type, type, InstanceType.Constant, instance, options);
            return parameter;
        }

        #endregion

        #region from,to singleton

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// シングルトン形式で指定されたクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsSingleton<FROM, TO>(this INeCoBuilder builder, SystemInstanceRegistrationOptions options)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Singleton, null, options);
            return parameter;
        }

        #endregion

        #region from,to transient

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// Resolve毎に新しいインスタンスとして生成して欲しいクラスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsTransient<FROM, TO>(this INeCoBuilder builder, SystemInstanceRegistrationOptions options)
        {
            IRegistrationParamter parameter = Registration(builder, typeof(FROM), typeof(TO), InstanceType.Transient, null, options);
            return parameter;
        }

        #endregion

        #region from,to constant

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, new SystemInstanceRegistrationOptions());
            return parameter;
        }

        /// <summary>
        /// 既に生成したインスタンスをコンテナに登録する
        /// </summary>
        public static IRegistrationParamter RegistrationAsConstant<FROM>(this INeCoBuilder builder, object instance, SystemInstanceRegistrationOptions options)
        {
            Type type = instance.GetType();

            IRegistrationParamter parameter = Registration(builder, typeof(FROM), type, InstanceType.Constant, instance, options);
            return parameter;
        }        

        #endregion

        private static IRegistrationParamter Registration(INeCoBuilder builder, Type from, Type to, InstanceType instanceType, object instance,  SystemInstanceRegistrationOptions options)
        {
            var info = CreateSystemInstanceInfo(
                from: new Dependencys(from, options.Id),
                to: to,
                instanceType: instanceType,
                instance: instance,
                options
            );

            builder.Register(info);
            return info;
        }

        private static IRegistrationParamter CreateSystemInstanceInfo(Dependencys from, Type to, InstanceType instanceType, object instance, SystemInstanceRegistrationOptions options)
        {
            if (instance != null && instance.GetType().IsMonoBehaviourSubClass())
                throw new NotSupportedException("MonoBehaviourを継承しているクラスを指定しました : " + instance.GetType());

            INeCoInjecter injecter = CreateInjecter(to, options.injectionType);

            return new SystemInstanceParameter
            (
                from,
                to,
                instanceType,
                injecter,
                instance,
                options.IsThisEntryPoint
            );
        }
    }
}