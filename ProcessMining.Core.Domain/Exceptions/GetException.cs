using ProcessMining.Core.Domain.BaseModels;

namespace ProcessMining.Core.Domain.Exceptions
{
    public class GetException<T> : Exception where T : DomainObject
    {
        public T Entity { get; }

        public GetException(T entity)
        {
            Entity = entity;
        }

        public GetException(string? message, T entity) : base(message)
        {
            Entity = entity;
        }

        public GetException(string? message, Exception? innerException, T entity) : base(message, innerException)
        {
            Entity = entity;
        }
    }
}
