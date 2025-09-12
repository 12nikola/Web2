using System;

namespace QuizWebServer.Exceptions
{
    public class EntityReferenceConflictException : Exception
    {
        public EntityReferenceConflictException(string message) : base(message) { }

        public EntityReferenceConflictException(string entity, string referencedById)
            : base($"{entity} with identifier '{referencedById}' is referenced by another entity.") { }
    }
}
