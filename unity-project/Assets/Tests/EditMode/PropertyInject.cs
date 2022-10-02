using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;

public class PropertyInject
{
    public class SampleClassA
    {
        [Inject]
        public SampleClassB B
        {
            get;
            private set;
        } = null;
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

    // A Test behaves as an ordinary method
    [Test]
    public void プロパティインジェクション_Singleton()
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
