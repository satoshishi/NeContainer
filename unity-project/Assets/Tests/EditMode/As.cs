using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;
using System;


public class As
{
    public interface ISampleA
    {

    }

    public interface ISampleB
    {

    }

    public class ImplementAB : ISampleA, ISampleB
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void 複数の型をkeyにresolveできる()
    {
        var builder = NeCoUtilities.Create();
        builder.RegistrationAsSingleton<ISampleA, ImplementAB>().Or<ISampleB>();

        var resolver = builder.Build();
        var a = resolver.Resolve<ISampleA>();
        var b = resolver.Resolve<ISampleB>();

        Assert.IsNotNull(a);
        Assert.IsNotNull(b);
    }
}
