using AbsencesAPI.Common.Model;
using System.Runtime.Serialization;

namespace AbsencesAPI.Business.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public int Id { get; }
    public string Type { get; }

    public NotFoundException()
    {
    }

    public NotFoundException(int id, string type)
    {
        Id = id;
        Type = type;
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
