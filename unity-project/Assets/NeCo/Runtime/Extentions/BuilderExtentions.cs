using System;
using System.Reflection;
using UnityEngine;
using NeCo.Recursion;

namespace NeCo
{
    public static partial class BuilderExtentions
    {
        #region factory

        private static INeCoInjecter CreateInjecter(Type target, InjectionType type)
        {
            INeCoInjecter newInjecter = default;        

            newInjecter = type switch
            {
                InjectionType.Constructor => CreateConstructorInjecter(target),
                InjectionType.Method => CreateMethodInjecter(target),
                InjectionType.Property => CreatePropertyInjecter(target),
                InjectionType.None => CreateDoNotInjecter(target),
                InjectionType.Self => CreateInjecterSelf(target),
                _ => CreateInjecterSelf(target),
            };            

            // 生成できなかった場合は、selfで無理やり作ろうとする
            if(newInjecter == null)
                return CreateInjecterSelf(target);

            return newInjecter;
        }

        private static INeCoInjecter CreateConstructorInjecter(Type target)
        {
            INeCoInjecter newInjecter = null;               

            if (target.HasInjectionAttributeInConstructor(out ConstructorInfo constructor))
            {
                newInjecter = new ConstructInjecter(constructor);

                return newInjecter;
            }        

            return null;
        }

        private static INeCoInjecter CreateMethodInjecter(Type target)
        {
            INeCoInjecter newInjecter = null;               

            if (target.HasInjectionAttributeInMethod(out MethodInfo method))
            {
                newInjecter = new MethodInjecter(method);
                
                return newInjecter;                
            }

            return null;
        }   

        private static INeCoInjecter CreatePropertyInjecter(Type target)
        {
            INeCoInjecter newInjecter = null;               

            if (target.HasInjectionAttributeInProperty(out (PropertyInfo, string)[] propertys))
            {
                newInjecter = new PropertyInjecter(propertys);

                return newInjecter;
            }

            return null;
        }                

        private static INeCoInjecter CreateDoNotInjecter(Type target)
        {
            INeCoInjecter newInjecter = new DoNotInjecter();

            return newInjecter;
        }                        

        private static INeCoInjecter CreateInjecterSelf(Type target)
        {
            INeCoInjecter newInjecter = null;               

            // コンストラクタから作ろうとする
            newInjecter = CreateConstructorInjecter(target);
            if(newInjecter != null)
                return newInjecter;

            // メソッドから作ろうとする
            newInjecter = CreateMethodInjecter(target);
            if(newInjecter != null)
                return newInjecter;                

            // プロパティから作ろうとする
            newInjecter = CreatePropertyInjecter(target);
            if(newInjecter != null)
                return newInjecter;                                

            // どれも作れなかったらinjection不要と判断
            return CreateDoNotInjecter(target);

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