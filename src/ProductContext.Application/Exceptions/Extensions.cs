namespace ProductContext.Application.Exceptions
{
    public static class Extensions
    {
        public static T OrThrow<T>(this T source,object id)
        {
            if (source == null)
            {
                throw new EntityNotFoundException<T>(id);
            }

            return source;
        }
    }
}