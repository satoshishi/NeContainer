using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;

public class IdInject
{
    public class SampleClassA
    {

    }

    public class SampleClassB
    {
        [InjectFromID("a")]
        public SampleClassA a { get; private set; } = null;

        [Inject]
        public SampleClassC c { get; set; } = null;
    }

    public class SampleClassC
    {

    }

    // A Test behaves as an ordinary method
    [Test]
    public void 同じ型を違うidで登録()
    {
        var builder = NeCoUtilities.Create();
        builder.Register<SampleClassA>(InstanceType.Singleton, "a");
        builder.Register<SampleClassA>(InstanceType.Singleton, "b");

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>("a");
        var b = resolver.Resolve<SampleClassA>("b");
        Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
    }

    // A Test behaves as an ordinary method
    [Test]
    public void idを指定して登録した場合resolveもidを指定しないと例外を吐く()
    {
        void ThrowCase(INeCoResolver resolver)
        {
            resolver.Resolve<SampleClassA>();
        }

        var builder = NeCoUtilities.Create();
        builder.Register<SampleClassA>(InstanceType.Singleton, "a");
        builder.Register<SampleClassA>(InstanceType.Singleton, "b");

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>("a");

        Assert.Throws<KeyNotFoundException>(() => ThrowCase(resolver));
    }

    // A Test behaves as an ordinary method
    [Test]
    public void 同じ型をidなしとありで登録できる()
    {
        var builder = NeCoUtilities.Create();
        builder.Register<SampleClassA>(InstanceType.Singleton, "a");
        builder.Register<SampleClassA>(InstanceType.Singleton);

        var resolver = builder.Build();
        var a = resolver.Resolve<SampleClassA>("a");
        var b = resolver.Resolve<SampleClassA>();
        Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Test]
    public void プロパティインジェクトはidが指定できる()
    {
        var builder = NeCoUtilities.Create();
        builder.Register<SampleClassA>(InstanceType.Singleton, "a");
        builder.Register<SampleClassA>(InstanceType.Singleton);
        builder.Register<SampleClassB>(InstanceType.Singleton);
        builder.Register<SampleClassC>(InstanceType.Singleton);
        
        var resolver = builder.Build();
        var a1 = resolver.Resolve<SampleClassA>("a");
        var a2 = resolver.Resolve<SampleClassA>();
        var b = resolver.Resolve<SampleClassB>();

        Assert.AreEqual(a1.GetHashCode(),b.a.GetHashCode());
        Assert.AreNotEqual(a2.GetHashCode(),b.a.GetHashCode());
        Assert.IsNotNull(b.c);
    }
}
