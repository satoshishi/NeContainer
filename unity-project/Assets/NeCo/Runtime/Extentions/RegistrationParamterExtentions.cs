namespace NeCo
{
    public static class RegistrationParamterExtentions
    {
        public static bool IsSingleton(this IRegistrationParamter info) => info.InstanceType == InstanceType.Singleton;

        public static bool IsTransient(this IRegistrationParamter info) => info.InstanceType == InstanceType.Transient;

        public static bool IsConstant(this IRegistrationParamter info) => info.InstanceType == InstanceType.Constant;

        internal static NeCoProvider CreateProvider(this IRegistrationParamter info)
        {
            if (info.To.IsMonoBehaviourSubClass())
            {
                if (info is MonoBehaviourInstanceParameter)
                    return new MonoBehaviourInstanceProvider(info as MonoBehaviourInstanceParameter);

                return new PrefabInstanceProvider(info as PrefabInstanceParameter);
            }

            if(info is FunctionInstanceParameter)
            {
                return new FunctionInstanceProvider(info as FunctionInstanceParameter);
            }

            return new SystemInstanceProvider(info as SystemInstanceParameter);
        }
    }
}