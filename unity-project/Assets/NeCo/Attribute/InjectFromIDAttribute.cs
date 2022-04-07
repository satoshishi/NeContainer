using System;

namespace NeCo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class InjectFromIDAttribute : Attribute
    {
        public string id;

        public InjectFromIDAttribute(string id)
        {
            this.id = id;
        }
    }
}
