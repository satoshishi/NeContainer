using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;
using System;

public class EntryPoint
{
    public class SampleA
    {
        public ISample B;

        public string tex;

        [Inject]
        public SampleA(ISample b, string text)
        {
            this.B = b;
            this.tex = text;
        }

        public void Say() => B.Say(tex);
    }

    public interface ISample
    {
        void Say(string text);
    }

    public class SampleB : ISample
    {
        public void Say(string text) => Debug.Log(text);
    }

    public class SampleEntry
    {
        [Inject]
        public SampleEntry(SampleA a)
        {
            a.Say();
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void BuildのタイミングでEntryPointがResolveされる()
    {
        var builder = _.Create();
        builder.RegistrationAsSingleton<SampleA>();
        builder.RegistrationAsSingleton<ISample, SampleB>();
        builder.RegistrationAsConstant("hello");
        builder.RegistrationAsSingleton<SampleEntry>(true);

        builder.Build();

        LogAssert.Expect(LogType.Log, "hello");
    }
}
