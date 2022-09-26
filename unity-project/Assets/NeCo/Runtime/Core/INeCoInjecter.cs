namespace NeCo
{
    using System;
    
    internal interface INeCoInjecter : IDisposable
    {
        Dependencys InjectionTarget 
        { 
            get; 
        }
        
        object Inject(object instance, ProviderCaches history, ProviderCaches caches);
    }
}