using System;

namespace QuizWebServer.Exceptions
{
    public class RequiredValuesMissingException : Exception
    {
        public RequiredValuesMissingException(string message) : base(message) { }

        public RequiredValuesMissingException(string entity, string id)
            : base($"Missing required values for {entity} with identifier '{id}'.") { }
    }
}
