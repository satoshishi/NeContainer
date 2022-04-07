using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{
    public interface IRegistrationHelper
    {
        INeCoResolver RegistrationAndBuild();
        
        INeCoBuilder Registration(INeCoBuilder container = default);
    }
}