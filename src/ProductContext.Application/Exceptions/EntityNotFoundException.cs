using System;

namespace ProductContext.Application.Exceptions
{
    public class EntityNotFoundException<TEntity>
        : Exception
    {
        public EntityNotFoundException(object id) : base(id.ToString())
        {
        }
    }
}