using System;

namespace QuizWebServer.Exceptions
{
    public class EntityConflictException : Exception
    {
        public EntityConflictException(string message) : base(message) { }

        public EntityConflictException(string entity, string conflictingId)
            : base($"{entity} with identifier '{conflictingId}' already exists.") { }
    }
}

