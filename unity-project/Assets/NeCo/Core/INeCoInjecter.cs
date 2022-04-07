namespace NeCo
{
    internal interface INeCoInjecter
    {
        Dependencys InjectionTarget 
        { 
            get; 
        }
        
        object Inject(object instance, ProviderCaches history, ProviderCaches caches);
    }
}