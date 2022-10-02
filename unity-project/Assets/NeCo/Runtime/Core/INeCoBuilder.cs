namespace NeCo
{
    using System;

    public enum InstanceType
    {
        Singleton,
        Transient,
        Constant
    }

    public interface INeCoBuilder : IDisposable
    {
        bool Vaild {get;}

        IRegistrationParamter Register(IRegistrationParamter info);        
        
        INeCoResolver Build();       
    }
}