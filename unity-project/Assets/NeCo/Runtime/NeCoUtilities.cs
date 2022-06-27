using NeCo.Recursion;

namespace NeCo
{
    public static class NeCoUtilities
    {
        public static INeCoBuilder Create()
        {
            return new RecursionBuilder();
        }
    }
}