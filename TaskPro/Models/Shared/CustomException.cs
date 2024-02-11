namespace TaskPro.Models.Shared
{
    public class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
    public class CustomException
    {
    }

    public class ValidationException : Exception
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "Error en validacion";
            }
        }
    }
    public class DataBaseException : Exception
    {
        public DataBaseException() { }
        public DataBaseException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "Error en peticion a la base de datos";
            }
        }
    }
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() { }
        public AlreadyExistException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "Ya existe un elemento guardado con esas carracterísticas";
            }
        }
    }
    public class UnknownUserException : Exception
    {
        public UnknownUserException() { }
        public UnknownUserException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "El usuario que intenta buscar no existe";
            }
        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "El objeto no existe";
            }
        }
    }
    public class DocumentException : Exception
    {
        public DocumentException() { }
        public DocumentException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "Error ocurrido al crear el documento";
            }
        }
    }
}
