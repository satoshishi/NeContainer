using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {
        public static IRegistrationParamter RegisterFunc_AsSingleton<INPUT, OUTPUT>(this INeCoBuilder builder, Func<INeCoResolver, Func<INPUT, OUTPUT>> func)
        {
            Type type = typeof(Func<INPUT, OUTPUT>);

            var info = CreateFuncInstanceInfo(
                from: new Dependencys(type, string.Empty),
                to: type,
                instanceType: InstanceType.Constant,
                instance: func
            );

            builder.Register(info);
            return info;
        }        

        private static IRegistrationParamter CreateFuncInstanceInfo(Dependencys from, Type to, InstanceType instanceType, Func<INeCoResolver, object> instance = null)
        {
            INeCoInjecter injecter = new FunctionInjecter();

            return new FunctionInstanceParameter(
                from,
                to,
                instanceType,
                injecter,
                instance,
                false);
        }        
    }
}