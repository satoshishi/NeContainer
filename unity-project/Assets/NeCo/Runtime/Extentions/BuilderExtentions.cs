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
            INeCoInjecter newInjecter = null;            

            //キャッシュがあったら、そっちを使う
            if(InjecterContainer.TryGet(target, out INeCoInjecter existsInjecter))
            {
                return existsInjecter;
            }

            if (!target.HasInjectionAttributeInAny())
            {
                newInjecter = new DoNotInjecter();
                InjecterContainer.Add(target, newInjecter);      

                return newInjecter;
            }                          

            if (target.HasInjectionAttributeInConstructor(out ConstructorInfo constructor))
            {
                newInjecter = new ConstructInjecter(constructor);
                InjecterContainer.Add(target, newInjecter);

                return newInjecter;
            }              

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
            {
                newInjecter = new MethodInjecter(method);
                InjecterContainer.Add(target, newInjecter);
                
                return newInjecter;                
            }

            if (target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
            {
                newInjecter = new PropertyInjecter(propertys);
                InjecterContainer.Add(target, newInjecter);      

                return newInjecter;
            }

            newInjecter = new DoNotInjecter();
            InjecterContainer.Add(target, newInjecter);      

            return newInjecter;
        }

        #endregion

        private static (Type, object) GetRegistrationTypeAndObject(string componentTypeName, MonoBehaviour instance)
        {
            Type type;
            object target;

            if (string.IsNullOrEmpty(componentTypeName))
            {
                type = instance.GetType();
                target = instance;
            }
            else
            {
                Component component = instance.gameObject.GetComponent(componentTypeName);
                type = component.GetType();
                target = component;
            }

            return (type, target);
        }
    }
}