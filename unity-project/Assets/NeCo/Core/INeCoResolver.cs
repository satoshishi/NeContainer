using System;

namespace NeCo
{
    public interface INeCoResolver
    {
        object Resolve(Type type, string id);       
    }
}