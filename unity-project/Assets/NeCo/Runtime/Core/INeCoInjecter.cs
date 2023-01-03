namespace NeCo
{
    using System;

    public enum InjectionType
    {
        Constructor,
        Method,
        Property,
        Self
    }

    internal interface INeCoInjecter : IDisposable
    {
        Dependencys InjectionTarget 
        { 
            get; 
        }
        
        object Inject(object instance, ProviderCaches history, ProviderCaches caches);
    }
}