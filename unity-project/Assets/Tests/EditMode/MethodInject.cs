using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;


public class MethodInject
{
    public class SampleClassA
    {
        public SampleClassB B
        {
            get;
            private set;
        } = null;        

        [Inject]
        public void Init(SampleClassB b)
        {
            B = b;
        }
    }

    public class SampleClassB
    {
        public int Value
        {
            get;
            private set;
        }

        [Inject]
        public SampleClassB()
        {
            Value = UnityEngine.Random.Range(0, 100);
        }
    } 

    [Test]
    public void メソッドインジェクション_Singleton()
    {
        var builder = NeCoUtilities.Create();
        builder.RegistrationAsSingleton<SampleClassB>();
        builder.RegistrationAsSingleton<SampleClassA>();

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>();
        var b = resolver.Resolve<SampleClassB>();
        Assert.AreEqual(a.B.Value, b.Value);
    }       
}
