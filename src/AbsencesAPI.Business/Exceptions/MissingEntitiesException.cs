using System.Runtime.Serialization;

namespace AbsencesAPI.Business.Exceptions;

[Serializable]
public class MissingEntitiesException : Exception
{
    public List<int> Ids { get; }
    public string Type { get; }

    public MissingEntitiesException()
    {
    }

    public MissingEntitiesException(string? message) : base(message)
    {
    }

    public MissingEntitiesException(List<int> ids, string type)
    {
        this.Ids = ids;
        this.Type = type;
    }

    public MissingEntitiesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected MissingEntitiesException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}