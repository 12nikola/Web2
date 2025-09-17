using System;

namespace KvizHub.Exceptions
{
    public class DeletionFailedException : Exception
    {
        public DeletionFailedException(string message) : base(message) { }

        public DeletionFailedException(string entity, string id)
            : base($"Failed to delete {entity} with identifier '{id}'. No changes were detected.") { }
    }
}
