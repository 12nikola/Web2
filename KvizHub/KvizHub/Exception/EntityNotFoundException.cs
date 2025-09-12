using System;

namespace QuizWebServer.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string entity, object id)
            : base($"{entity} with key '{id}' was not found.") { }
    }
}
