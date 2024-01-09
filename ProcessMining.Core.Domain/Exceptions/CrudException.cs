using ProcessMining.Core.Domain.BaseModels;

namespace ProcessMining.Core.Domain.Exceptions
{
    public class CrudException<T> : Exception where T : DomainObject
    {
        public T Entity { get; }

        public CrudException(T entity)
        {
            Entity = entity;
        }

        public CrudException(string? message, T entity) : base(message)
        {
            Entity = entity;
        }

        public CrudException(string? message, Exception? innerException, T entity) : base(message, innerException)
        {
            Entity = entity;
        }
    }
}
