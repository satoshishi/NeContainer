using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;
using System;

public class ConstructInject
{
    public class SampleClassA
    {
        public SampleClassB B;

        [Inject]
        public SampleClassA(SampleClassB b)
        {
            this.B = b;
        }
    }

    public class SampleClassB
    {
        public int value { get; private set; }

        [Inject]
        public SampleClassB()
        {
            value = UnityEngine.Random.Range(0, 100);
        }
    }

    public class SampleClassC
    {
        public SampleClassD D;

        public int value { get; private set; }

        [Inject]
        public SampleClassC(SampleClassD d)
        {
            value = UnityEngine.Random.Range(0, 100);
            this.D = d;
        }

        public void Say()
        {
            Debug.Log(value + " " + D.value);
        }
    }

    public class SampleClassD
    {
        public SampleClassC C;

        public int value { get; private set; }

        [Inject]
        public SampleClassD(SampleClassC c)
        {
            value = UnityEngine.Random.Range(0, 100);
            this.C = c;
        }

        public void Say()
        {
            Debug.Log(value + " " + C.value);
        }
    }

    public class SampleClassE
    {
        public SampleClassF f;

        [Inject]
        public SampleClassE(SampleClassF f)
        {
            this.f = f;
        }
    }

    public class SampleClassF
    {
        public int value { get; private set; }

        public SampleClassF()
        {
            value = UnityEngine.Random.Range(0, 100);
        }
    }

    public class SampleClassG
    {
        public SampleClassH h;

        [Inject]
        public SampleClassG(SampleClassH h)
        {
            this.h = h;
        }

        public SampleClassG()
        {

        }
    }

    public class SampleClassH
    {
        public SampleClassG g;

        [Inject]
        public SampleClassH(SampleClassG g)
        {
            this.g = g;
        }
    }


    [Test]
    public void コストラクタインジェクション_Singleton()
    {
        var builder = NeCoUtilities.Create();

        builder.Register<SampleClassA>(InstanceType.Singleton);
        builder.Register<SampleClassB>(InstanceType.Singleton);

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>();
        var b = resolver.Resolve<SampleClassB>();

        Assert.AreEqual(a.B.value, b.value);
    }

    [Test]
    public void コストラクタインジェクション_循環参照あり_Singleton()
    {
        var builder = NeCoUtilities.Create();

        builder.Register<SampleClassC>(InstanceType.Singleton);
        builder.Register<SampleClassD>(InstanceType.Singleton);

        var resolver = builder.Build();

        var c = resolver.Resolve<SampleClassC>();
        var d = resolver.Resolve<SampleClassD>();
        LogAssert.Expect(LogType.Log, c.value + " " + d.value);
        LogAssert.Expect(LogType.Log, d.value + " " + c.value);
        c.Say();
        d.Say();
    }

    [Test]
    public void コストラクタインジェクション_Transient()
    {
        var builder = NeCoUtilities.Create();

        builder.Register<SampleClassA>(InstanceType.Singleton);
        builder.Register<SampleClassB>(InstanceType.Transient);

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>();
        var b = resolver.Resolve<SampleClassB>();

        Debug.Log(a.B.value + " " + b.value);

        Assert.AreNotEqual(a.B.value, b.value);
    }

    [Test]
    public void コストラクタインジェクション_循環参照あり_Transient()
    {
        var builder1 = NeCoUtilities.Create();

        builder1.Register<SampleClassC>(InstanceType.Singleton);
        builder1.Register<SampleClassD>(InstanceType.Transient);

        var resolver1 = builder1.Build();

        var c = resolver1.Resolve<SampleClassC>();
        Assert.NotNull(c.D);
        Assert.NotNull(c.D.C);

        var d = resolver1.Resolve<SampleClassD>();
        Assert.NotNull(d.C);
        Assert.NotNull(d.C.D);



        var builder2 = NeCoUtilities.Create();

        builder2.Register<SampleClassC>(InstanceType.Singleton);
        builder2.Register<SampleClassD>(InstanceType.Transient);

        var resolver2 = builder2.Build();

        var d2 = resolver2.Resolve<SampleClassD>();
        Assert.NotNull(d2.C);
        Assert.Null(d2.C.D);

        var c2 = resolver2.Resolve<SampleClassC>();
        Assert.Null(c2.D);
    }

    [Test]
    public void コストラクタインジェクション_Constant()
    {
        var builder = NeCoUtilities.Create();

        builder.Register<SampleClassE>(InstanceType.Singleton);

        SampleClassF instance = new SampleClassF();
        builder.Register(instance);

        var resolver = builder.Build();
        var e = resolver.Resolve<SampleClassE>();
        var f = resolver.Resolve<SampleClassF>();

        Assert.AreEqual(e.f.value, f.value);
    }

    [Test]
    public void コストラクタインジェクション_循環参照あり_Constant()
    {
        var builder = NeCoUtilities.Create();

        SampleClassG instance = new SampleClassG();
        builder.Register(instance);
        builder.Register<SampleClassH>(InstanceType.Singleton);

        var resolver = builder.Build();

        var g = resolver.Resolve<SampleClassG>();
        var h = resolver.Resolve<SampleClassH>();

        Assert.AreEqual(g.GetHashCode(), h.g.GetHashCode());
        Assert.AreEqual(h.GetHashCode(), g.h.GetHashCode());
    }
}
