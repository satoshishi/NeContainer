namespace NeCo
{
    public static class ResolverExtentions
    {
        public static TO Resolve<TO>(this INeCoResolver resolver)
        {
            var to = resolver.Resolve(typeof(TO),"");
            return (TO)(to);
        }

        public static TO Resolve<TO>(this INeCoResolver resolver,string id)
        {
            var to = resolver.Resolve(typeof(TO),id);
            return (TO)(to);
        }        
    }
}