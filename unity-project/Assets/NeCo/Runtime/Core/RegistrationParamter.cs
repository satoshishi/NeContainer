using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace NeCo
{
    #region interface

    public interface IRegistrationParamter
    {
        /// <summary>
        /// 依存元
        /// </summary>
        /// <value></value>
        internal Dependencys From
        {
            get;
        }

        /// <summary>
        /// 依存先
        /// </summary>
        /// <value></value>
        Type To
        {
            get;
        }

        /// <summary>
        /// インスタンスの生成方法
        /// </summary>
        /// <value></value>
        InstanceType InstanceType
        {
            get;
        }

        /// <summary>
        /// 依存注入クラス
        /// </summary>
        /// <value></value>
        internal INeCoInjecter Injecter
        {
            get;
        }

        public bool IsThisEntryPoint
        {
            get;
        }
    }

    internal class Dependencys : Caches<(Type,string),Dependencys.Source>
    {
        public class Source
        {
            private Type type = null;
            public Type Type => type;

            private string id = null;
            public string Id => id;

            public Source(Type type, string id)
            {
                this.type = type;
                this.id = id;
            }
        }

        public Source[] Sources => caches;

        public Dependencys(Type type, string id)
        {
            caches = new Source[]{new Source(type,id)};
        }

        public Dependencys()
        {
            caches = new Source[]{};
        }

        public void Add(Type type, string id)
        {
            if(!Match((type,id),out Source value))
            {
                var newSource = new Source(type,id);
                var newCaches = new Source[caches.Length + 1];
                System.Array.Copy(caches, newCaches, caches.Length);
                newCaches[caches.Length] = newSource;

                caches = newCaches;
            }
        }

        public override bool Match((Type, string) key, out Source value)
        {
            foreach(var source in caches)
            {
                if(source.Type == key.Item1 && source.Id == key.Item2)
                {
                    value = source;
                    return true;
                }
            }

            value = null;
            return false;
        }
    }

    #endregion

    #region implements

    internal sealed class SystemInstanceParameter : IRegistrationParamter
    {
        public object Instance
        {
            get => instance;
        }
        private object instance;

        /// <summary>
        /// 依存元
        /// </summary>
        /// <value></value>
        Dependencys IRegistrationParamter.From
        {
            get => from;
        }
        private Dependencys from;

        /// <summary>
        /// 依存先
        /// </summary>
        /// <value></value>
        public Type To
        {
            get => to;
        }
        private Type to;

        /// <summary>
        /// インスタンスの生成方法
        /// </summary>
        /// <value></value>
        public InstanceType InstanceType
        {
            get => instanceType;
        }
        private InstanceType instanceType;

        /// <summary>
        /// 依存注入クラス
        /// </summary>
        /// <value></value>
        INeCoInjecter IRegistrationParamter.Injecter
        {
            get => injecter;
        }
        private INeCoInjecter injecter;

        public bool IsThisEntryPoint
        {
            get => isThisEntryPoint;
        }
        private bool isThisEntryPoint;

        public SystemInstanceParameter(
            Dependencys from,
            Type to,
            InstanceType instanceType,
            INeCoInjecter injecter,
            object instance,
            bool isThisEntryPoint)
        {
            this.from = from;
            this.to = to;
            this.instanceType = instanceType;
            this.injecter = injecter;
            this.instance = instance;
            this.isThisEntryPoint = isThisEntryPoint;
        }

        public SystemInstanceParameter(Type from)
        {
            this.from = new Dependencys(from,"");
        }
    }

    internal sealed class FunctionInstanceParameter : IRegistrationParamter
    {
        public Func<INeCoResolver, object> Instance
        {
            get => instance;
        }
        private Func<INeCoResolver, object> instance;

        /// <summary>
        /// 依存元
        /// </summary>
        /// <value></value>
        Dependencys IRegistrationParamter.From
        {
            get => from;
        }
        private Dependencys from;

        /// <summary>
        /// 依存先
        /// </summary>
        /// <value></value>
        public Type To
        {
            get => to;
        }
        private Type to;

        /// <summary>
        /// インスタンスの生成方法
        /// </summary>
        /// <value></value>
        public InstanceType InstanceType
        {
            get => instanceType;
        }
        private InstanceType instanceType;

        /// <summary>
        /// 依存注入クラス
        /// </summary>
        /// <value></value>
        INeCoInjecter IRegistrationParamter.Injecter
        {
            get => injecter;
        }
        private INeCoInjecter injecter;

        public bool IsThisEntryPoint
        {
            get => isThisEntryPoint;
        }
        private bool isThisEntryPoint;

        public FunctionInstanceParameter(
            Dependencys from,
            Type to,
            InstanceType instanceType,
            INeCoInjecter injecter,
            Func<INeCoResolver, object> instance,
            bool isThisEntryPoint)
        {
            this.from = from;
            this.to = to;
            this.instanceType = instanceType;
            this.injecter = injecter;
            this.instance = instance;
            this.isThisEntryPoint = isThisEntryPoint;
        }

        public FunctionInstanceParameter(Type from)
        {
            this.from = new Dependencys(from,"");
        }
    }    

    internal sealed class PrefabInstanceParameter : IRegistrationParamter
    {
        public MonoBehaviour GameObject
        {
            get;
            private set;
        }

        public Transform Parent
        {
            get;
            private set;
        }

        /// <summary>
        /// 依存元
        /// </summary>
        /// <value></value>
        Dependencys IRegistrationParamter.From
        {
            get => from;
        }
        private Dependencys from;

        /// <summary>
        /// 依存先
        /// </summary>
        /// <value></value>
        public Type To
        {
            get => to;
        }
        private Type to;

        /// <summary>
        /// インスタンスの生成方法
        /// </summary>
        /// <value></value>
        public InstanceType InstanceType
        {
            get => instanceType;
        }
        private InstanceType instanceType;

        /// <summary>
        /// 依存注入クラス
        /// </summary>
        /// <value></value>
        INeCoInjecter IRegistrationParamter.Injecter
        {
            get => injecter;
        }
        private INeCoInjecter injecter;


        public bool DontDestoryOnLoad
        {
            get => dontDestoryOnLoad;
        }
        private bool dontDestoryOnLoad;

        public bool IsThisEntryPoint
        {
            get => isThisEntryPoint;
        }
        private bool isThisEntryPoint;

        public PrefabInstanceParameter(
            Dependencys from,
            Type to,
            InstanceType instanceType,
            INeCoInjecter injecter,
            MonoBehaviour gameObject,
            Transform parent,
            bool dontDestoryOnLoad,
            bool isThisEntryPoint)
        {
            this.from = from;
            this.to = to;
            this.instanceType = instanceType;
            this.injecter = injecter;
            this.GameObject = gameObject;
            this.Parent = parent;
            this.dontDestoryOnLoad = dontDestoryOnLoad;
            this.isThisEntryPoint = isThisEntryPoint;
        }        
    }

    internal sealed class MonoBehaviourInstanceParameter : IRegistrationParamter
    {
        public MonoBehaviour GameObject
        {
            get;
            private set;
        }

        /// <summary>
        /// 依存元
        /// </summary>
        /// <value></value>
        Dependencys IRegistrationParamter.From
        {
            get => from;
        }
        private Dependencys from;

        /// <summary>
        /// 依存先
        /// </summary>
        /// <value></value>
        public Type To
        {
            get => to;
        }
        private Type to;

        /// <summary>
        /// インスタンスの生成方法
        /// </summary>
        /// <value></value>
        public InstanceType InstanceType
        {
            get => instanceType;
        }
        private InstanceType instanceType;

        /// <summary>
        /// 依存注入クラス
        /// </summary>
        /// <value></value>
        INeCoInjecter IRegistrationParamter.Injecter
        {
            get => injecter;
        }
        private INeCoInjecter injecter;

        public bool IsThisEntryPoint
        {
            get => isThisEntryPoint;
        }
        private bool isThisEntryPoint;

        public MonoBehaviourInstanceParameter(
            Dependencys from,
            Type to,
            InstanceType instanceType,
            INeCoInjecter injecter,
            MonoBehaviour gameObject,
            bool isThisEntryPoint)
        {
            this.from = from;
            this.to = to;
            this.instanceType = instanceType;
            this.injecter = injecter;
            this.GameObject = gameObject;
            this.isThisEntryPoint = isThisEntryPoint;
        }
    }

    #endregion
}