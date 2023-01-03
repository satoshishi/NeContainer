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

            //キャッシュがあったら、そっちを使う
            if(InjecterContainer.TryGet(target, out INeCoInjecter existsInjecter))
            {
                return existsInjecter;
            }

            newInjecter = type switch
            {
                InjectionType.Constructor => CreateConstructorInjecter(target),
                InjectionType.Method => CreateMethodInjecter(target),
                InjectionType.Property => CreatePropertyInjecter(target),
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
                InjecterContainer.Add(target, newInjecter);

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
                InjecterContainer.Add(target, newInjecter);
                
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
                InjecterContainer.Add(target, newInjecter);      

                return newInjecter;
            }

            return null;
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