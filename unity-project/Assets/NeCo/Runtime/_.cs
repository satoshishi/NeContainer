using NeCo.Recursion;

namespace NeCo
{
    public static class _
    {
        public static INeCoBuilder Create()
        {
            return new RecursionBuilder();
        }
    }
}