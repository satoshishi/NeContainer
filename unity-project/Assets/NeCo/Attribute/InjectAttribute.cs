using System;

namespace NeCo
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class InjectAttribute : Attribute
    {

    }
}