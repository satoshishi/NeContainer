using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {
        #region factory

        private static INeCoInjecter CreateInjecter(Type target)
        {
            if (target.HasInjectionAttributeInConstructor(out ConstructorInfo constructor))
                return new ConstructInjecter(constructor);

            //if (target.HasConstructor(out constructor))
            //return new ConstructInjecter(constructor);                

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
                return new MethodInjecter(method);

            if (target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
                return new PropertyInjecter(propertys);

            return new DoNotInjecter();
        }

        #endregion        
    }
}