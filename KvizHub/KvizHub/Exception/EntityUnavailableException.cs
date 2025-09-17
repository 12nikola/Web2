using System;

namespace KvizHub.Exceptions
{
    public class EntityUnavailableException : Exception
    {
        public EntityUnavailableException(string message) : base(message) { }

        public EntityUnavailableException(string entity, string id)
            : base($"{entity} with identifier '{id}' is not available.") { }
    }
}
