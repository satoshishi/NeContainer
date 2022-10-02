using System;

namespace NeCo
{
    public interface INeCoResolver : IDisposable
    {
        object Resolve(Type type, string id);       
    }
}