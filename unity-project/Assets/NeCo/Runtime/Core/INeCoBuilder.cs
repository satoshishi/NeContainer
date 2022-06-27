namespace NeCo
{
    public enum InstanceType
    {
        Singleton,
        Transient,
        Constant
    }

    public interface INeCoBuilder
    {
        IRegistrationParamter Register(IRegistrationParamter info);        
        
        INeCoResolver Build();       
    }
}