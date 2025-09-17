using System;

namespace KvizHub.Exceptions
{
    public class SaveFailedException : Exception
    {
        public SaveFailedException(string message) : base(message) { }

        public SaveFailedException(string entity, string id)
            : base($"Failed to save {entity} with identifier '{id}'. No changes were detected.") { }
    }
}