using System;
using UnityEngine;
using NeCo;

public class ServiceLocator : MonoBehaviour
{
    private INeCoResolver resolver;

    public static ServiceLocator Instance { get; private set; } = null;

    internal void Init(INeCoResolver resolver)
    {
        this.resolver = resolver;
        Instance = this;
    }

    public T Get<T>()
    {
        var service = resolver.Resolve<T>();

        if (service == null)
            throw new NullReferenceException("リクエストしたServiceが見つかりませんでした");

        return service;
    }

    public T Get<T>(string id)
    {
        var service = resolver.Resolve<T>(id);

        if (service == null)
            throw new NullReferenceException("リクエストしたServiceが見つかりませんでした");

        return service;
    }
}
