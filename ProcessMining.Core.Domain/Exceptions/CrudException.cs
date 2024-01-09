using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.Enums;

namespace ProcessMining.Core.Domain.Exceptions
{
    public class CrudException<T> : Exception where T : DomainObject
    {
        public T Entity { get; }
        public BaseOperations BaseOperation { get; }

        public CrudException(T entity, BaseOperations baseOperation)
        {
            Entity = entity;
            BaseOperation = baseOperation;
        }

        public CrudException(string? message, T entity, BaseOperations baseOperation) : base(message)
        {
            Entity = entity;
            BaseOperation = baseOperation;
        }

        public CrudException(string? message, Exception? innerException, T entity, BaseOperations baseOperation) : base(message, innerException)
        {
            Entity = entity;
            BaseOperation = baseOperation;
        }
    }
}
